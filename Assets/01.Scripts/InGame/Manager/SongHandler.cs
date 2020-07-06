using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SongHandler : MonoBehaviour
{
    private AudioSource audioSource;
    
    [Header("Objects")]
    [SerializeField]
    private AudioSource metronomeSource;

    private double offset;

    private double oneBeatTime;
    private double nextStep;

    private double metoronomeBeatTime;
    private double nextMetoronomeStep;

    private string[] mapFileStrings;

    private Dictionary<string, double> gameSettings = new Dictionary<string, double>(); 
    private List<SongProcessAction> songProgressActions = new List<SongProcessAction>();
    
    private void Awake(){
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = GameManager.instance.SelectSong.audioClip;

        ReadFile(); 
        
        double offsetForSample;

        offset = (gameSettings["Delay"] / 1000);
        offset.Log();
        
        offsetForSample = offset * audioSource.clip.frequency;
        offsetForSample.Log();
        
        oneBeatTime = (60.0 / gameSettings["BPM"]) / gameSettings["Split"];
        metoronomeBeatTime = (60.0 / gameSettings["BPM"]);
        
        nextStep = offsetForSample;
        nextMetoronomeStep = offsetForSample;

        audioSource.clip.frequency.Log();
        audioSource.clip.samples.Log();

        audioSource.Play();
    }

    private void Update(){
        if(audioSource.timeSamples >= nextStep){
            NodeGenerate().Start(this);
        }

        if(audioSource.timeSamples >= nextMetoronomeStep){
            Metoronome().Start(this);
        }
    }

    private IEnumerator NodeGenerate(){
        if(songProgressActions.Count <= 0){
            yield break;
        }

        int beforePosition = 0;

        do{
            songProgressActions[0].action();
            beforePosition = songProgressActions[0].position;
            songProgressActions.RemoveAt(0);
        }while(beforePosition != 0 && songProgressActions[0].position.Equals(beforePosition));
                
        nextStep += oneBeatTime * audioSource.clip.frequency;
        
        yield return YieldInstructionCache.WaitFrame;
    }

    private IEnumerator Metoronome(){
        metronomeSource.Play();

        nextMetoronomeStep += metoronomeBeatTime * audioSource.clip.frequency;
        yield return YieldInstructionCache.WaitFrame;
    }

    private void ReadFile(){
        var tempString = GameManager.instance.SelectSong.mapTextAsset.text;


        mapFileStrings = tempString.Split('\n');
        
        for(int i = 0; i < mapFileStrings.Length; i++){
            bool addedCommand = false;

            if(mapFileStrings[i].StartsWith(":")){
                var settingStrings = mapFileStrings[i].Split(':')[1].Split('=');
  
                if(!gameSettings.ContainsKey(settingStrings[0])){
                    gameSettings.Add(settingStrings[0], double.Parse(settingStrings[1]));                
                    continue;
                }

                SongProcessAction newAction = new SongProcessAction();
                
                newAction.action = GameSettingAction(settingStrings[0], double.Parse(settingStrings[1]));
                newAction.position = -1;
    
                songProgressActions.Add(newAction);
                continue;
            }   
            
            Action<string, Func<int, Action>> addProgressAction = (value, nodeAction) => {
                var nodePositions = mapFileStrings[i].IndexOfMany(value);
                
                SongProcessAction.generateSequence++;

                for(int j = 0; j < nodePositions.Length; j++){
                    SongProcessAction newAction = new SongProcessAction();
                    
                    newAction.action = nodeAction(nodePositions[j]);
                    newAction.position = SongProcessAction.generateSequence;

                    songProgressActions.Add(newAction); 
                    
                    addedCommand = true;   
                }
            };

            addProgressAction("X", NormalNodeGenerateAction);
            addProgressAction("M", LongNodeStartGenerateAction);
            addProgressAction("W", LongNodeEndGenerateAction);

            if(!addedCommand){
                SongProcessAction newAction = new SongProcessAction();

                newAction.action = () => {};
                newAction.position = 0;
                
                songProgressActions.Add(newAction);
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

    private Action GameSettingAction(string valueName, double value){
        return () => {
            Setting(valueName, value);
        };
    }

    private void Setting(string valueName, double value){
        gameSettings[valueName] = value;
    }
}
