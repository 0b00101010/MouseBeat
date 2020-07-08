using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{

    public static MouseInputHandler instance;

    private IKeyObserver observer;

    private List<KeyCode> pressedKeys = new List<KeyCode>();
    private List<KeyCode> actionKeys = new List<KeyCode>();
    private List<KeyCode> releasedKeys = new List<KeyCode>();

    private void Awake(){
        if(instance is null){
            instance = this;
        }
    }

    private void Update(){
        if(Input.anyKeyDown || Input.anyKey){
            foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode))){
                if(Input.GetKey(key)){
                    if(!actionKeys.Contains(key)){
                        observer.KeyDownNotify(key);
                        actionKeys.Add(key);
                    }

                    pressedKeys.Add(key);
                }
            }
        }

        if(actionKeys.Count > 0){
            actionKeys.ForEach((key) => {            
                releasedKeys.Add(key);
            });
        }

        for(int i = 0; i < releasedKeys.Count; i++){
            if(!pressedKeys.Contains(releasedKeys[i])){
                observer.KeyUpNotify(releasedKeys[i]);
                releasedKeys.Remove(releasedKeys[i]);
            } else {
                observer.KeyHoldingNotify(releasedKeys[i]);
            }

            if(releasedKeys.Count <= 0){
                break;
            }
        }

        if(releasedKeys.Count > 0){
            actionKeys.Clear();

            releasedKeys.ForEach((key) => {
                actionKeys.Add(key);
            });
        } else {
            actionKeys.Clear();
        }

        releasedKeys.Clear();
        pressedKeys.Clear();
    }

    public void AddObserver(IKeyObserver observer){
        this.observer = observer;
    }

    public void RemoveObserver(IKeyObserver observer){
        this.observer = this.observer.Equals(observer) ? null : this.observer;
    }

    private void OnDestroy(){
        instance = null;
    }
}
