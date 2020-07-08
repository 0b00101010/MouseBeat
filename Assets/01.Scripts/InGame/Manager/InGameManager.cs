using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{

    public static InGameManager instance;

    [HideInInspector]
    public ScoreManager scoreManager;
    
    [HideInInspector]
    public NodeInteractionController nodeInteractionController;
    
    [HideInInspector]
    public NodeGenerator nodeGenerator;
    
    private BackgroundController backgroundController;

    private void Awake(){
        if(instance is null){
            instance = this;
        }

        Cursor.visible = false;

        backgroundController = gameObject.GetComponent<BackgroundController>();
        nodeInteractionController = gameObject.GetComponent<NodeInteractionController>();
        nodeGenerator = gameObject.GetComponent<NodeGenerator>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    [Button("Change Background Color")]
    public void ChangeBackgroundColor() { 
        backgroundController.Execute();
    }

    public void GameEnd(){
        Cursor.visible = true; 
        instance = null;
        SceneManager.LoadScene("00.StartScene");
    }
}
