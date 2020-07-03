using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SongHandler : MonoBehaviour
{
    private AudioSource audioSource;
    private float songProcess;
    
    [Header("Values")]
    [SerializeField]
    private float delayTime;
    private float waitingTime;

    private string[] mapFileStrings;

    private Dictionary<string, float> gameSettings = new Dictionary<string, float>(); 
    private List<SongProcessAction> songProgressActions = new List<SongProcessAction>();
    
    private void Awake(){
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start(){
        ReadFile(); 
        // delayTime = (gameSettings["Delay"] / 1000);
        waitingTime = 60 / gameSettings["BPM"] / gameSettings["Split"];
        
        audioSource.Play();
        SongGenerateCoroutine().Start(this);
    }

    private IEnumerator SongGenerateCoroutine(){
        yield return YieldInstructionCache.WaitRealSeconds(delayTime);
        
        while(songProgressActions.Count > 0){
            if(songProgressActions[0].position != -1){
                int beforePosition; 
            
                do{
                    songProgressActions[0].action();
                    beforePosition = songProgressActions[0].position;
                    songProgressActions.RemoveAt(0);
                }while(beforePosition != 0 && songProgressActions[0].position.Equals(beforePosition));
                
                yield return YieldInstructionCache.WaitRealSeconds(waitingTime);
            } else {
                delayTime = 0;

                do{
                    songProgressActions[0].action();
                    songProgressActions.RemoveAt(0);
                }while(songProgressActions[0].position == -1);
                
                delayTime = gameSettings["Delay"] / 1000 / audioSource.clip.length;
                waitingTime = 60 / gameSettings["BPM"] / gameSettings["Split"];

                if(delayTime > 0){
                    yield return YieldInstructionCache.WaitRealSeconds(delayTime);
                }
            }
        }
    }
    
    private void ReadFile(){
        var tempString = GameManager.instance.SelectSong.mapTextAsset.text;

        audioSource.clip = GameManager.instance.SelectSong.audioClip;

        mapFileStrings = tempString.Split('\n');
        
        for(int i = 0; i < mapFileStrings.Length; i++){
            bool addedCommand = false;

            if(mapFileStrings[i].StartsWith(":")){
                var settingStrings = mapFileStrings[i].Split(':')[1].Split('=');
  
                if(!gameSettings.ContainsKey(settingStrings[0])){
                    gameSettings.Add(settingStrings[0], float.Parse(settingStrings[1]));                
                    continue;
                }

                SongProcessAction newAction = new SongProcessAction();
                
                newAction.action = GameSettingAction(settingStrings[0], float.Parse(settingStrings[1]));
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

    private Action GameSettingAction(string valueName, float value){
        return () => {
            Setting(valueName, value);
        };
    }

    private void Setting(string valueName, float value){
        gameSettings[valueName] = value;
    }
}
