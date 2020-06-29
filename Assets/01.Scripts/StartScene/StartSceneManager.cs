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
    private SongInformation currentSongInformation;

    public void Update(){
        if(Input.GetMouseButtonDown(0)){
            if(isTitle){
                titleWidget.CloseWidget(() => {
                    stageSelectWidget.OpenWidget();
                });
                isTitle = false;
            } else {
                SongInformation selectSongInformation = GetSongInformation();
                if(selectSongInformation != null){
                    currentSongInformation?.Exit();
                    selectSongInformation.Execute();

                    currentSongInformation = selectSongInformation;
                }
            }
        }
    }

    private SongInformation GetSongInformation(){
        Ray ray = new Ray();
        
        ray.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ray.direction = Vector2.zero;

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Song"));

        if(hit.collider != null){
            return hit.collider.GetComponent<SongInformation>();
        }

        return null;
    }    
}
