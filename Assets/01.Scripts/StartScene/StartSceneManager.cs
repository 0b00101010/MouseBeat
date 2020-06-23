using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSceneManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Image[] logoImages;

    [SerializeField]
    private UIWidget titleWidget;

    [SerializeField]
    private UIWidget stageSelectWidget; 

    private bool isTitle = true;

    public void Update(){
        if(Input.GetMouseButtonDown(0)){
            if(isTitle){
                titleWidget.CloseWidget(stageSelectWidget.OpenWidget);
            } else {
                
            }
        }
    }
}
