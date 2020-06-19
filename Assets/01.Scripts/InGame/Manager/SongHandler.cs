using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongHandler : MonoBehaviour
{
    private AudioSource audioSource;
    private float songProcess;
    
    private float delayTime;

    private string[] mapFileStrings;

    private Dictionary<string, float> gameSettings = new Dictionary<string, float>(); 
    private List<SongProcessAction> songAction = new List<SongProcessAction>();

    private void Start(){
        audioSource = gameObject.GetComponent<AudioSource>();
        ReadFile(); 
        Delay().Start(this);
        this.enabled = false;
    }

    private IEnumerator Delay(){
        delayTime = gameSettings["Delay"] / 1000 / audioSource.clip.length;
        yield return YieldInstructionCache.WaitRealSeconds(gameSettings["Delay"] / 1000);
        this.enabled = true;
    }

    private void Update(){
        songProcess = (audioSource.time / audioSource.clip.length) - delayTime;
        
        NodeGenerate();
        NodeGenerate();
    
    }

    private void NodeGenerate(){
        if(songAction[0].position < songProcess){
            songAction[0].action();
            songAction.Remove(songAction[0]);
        }
    }

    private void ReadFile(){
        var tempString = Resources.Load<TextAsset>("MapFile/StepEdit").text;
        mapFileStrings = tempString.Split('\n');
        
        var count = 0;

        for(int i = 0; i < mapFileStrings.Length; i++){
            if(!mapFileStrings[i].StartsWith(":")){
                count = i;
            }
        }

        for(int i = 0; i < mapFileStrings.Length; i++){
            if(!mapFileStrings[i].StartsWith(":")){
                float processValue = (float)i / (float)count;
                
                List<int> positionValues = mapFileStrings[i].IndexOfMany("X").ToList();

                if(positionValues.Count > 0){
                    for(int j = 0; j < positionValues.Count; j++){
                        SongProcessAction processAction = new SongProcessAction();

                        processAction.position = processValue;
                        processAction.action = NormalNodeGenerateAction(positionValues[j]);
                        
                        songAction.Add(processAction);
                    }
                }

                positionValues.Clear();

                positionValues = mapFileStrings[i].IndexOfMany("M").ToList();

                if(positionValues.Count > 0){
                    for(int j = 0; j < positionValues.Count; j++){
                        SongProcessAction processAction = new SongProcessAction();

                        processAction.position = processValue;
                        processAction.action = LongNodeStartGenerateAction(positionValues[j]);
                        
                        songAction.Add(processAction);
                    }
                }

                positionValues.Clear();

                positionValues = mapFileStrings[i].IndexOfMany("W").ToList();

                if(positionValues.Count > 0){
                    for(int j = 0; j < positionValues.Count; j++){
                        SongProcessAction processAction = new SongProcessAction();

                        processAction.position = processValue;
                        processAction.action = LongNodeEndGenerateAction(positionValues[j]);
                        
                        songAction.Add(processAction);
                    }
                }

            } else {
                var tempStrings = mapFileStrings[i].Split(':')[1].Split('=');
                
                if(!gameSettings.ContainsKey(tempStrings[0])){
                    gameSettings.Add(tempStrings[0], float.Parse(tempStrings[1]));
                } else {
                    GameSettingAction(tempStrings[0], float.Parse(tempStrings[1]));

                }
            }
        }
    }

    private Action NormalNodeGenerateAction(int positionValue){
        return () => {
            InGameManager.instance.nodeGenerator.NormalNodeGenerate(positionValue);
        };
    }

    private Action LongNodeStartGenerateAction(int positionValue){
        return () => {
            InGameManager.instance.nodeGenerator.LongNodeGenerate(positionValue);
        };
    }

    private Action LongNodeEndGenerateAction(int positionValue){
        return () => {
            InGameManager.instance.nodeGenerator.LongNodeStop(positionValue);
        };
    }

    private Action GameSettingAction(string valueName, float value){
        return () => {
            Setting(valueName, value);
        };
    }

    private void Setting(string valueName, float value){
        
        gameSettings[valueName] = value;
    }
}
