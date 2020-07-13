using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Image[] logoImages;

    [SerializeField]
    private UIWidget titleWidget;

    [SerializeField]
    private UIWidget stageSelectWidget; 

    [SerializeField]
    private UIWidget resultSelectWidet;

    [SerializeField]
    private Image blackFadeImage;

    private bool isTitle = true;
    private static bool doingTutorial;
    public static bool DoingTutorial {get => doingTutorial; set {doingTutorial = value;}}
    private SongInformation currentSongInformation;

    private void Start(){
        if(GameManager.instance.GameResult is null){
            titleWidget.OpenWidget();
        }

        else {
            isTitle = false;
            resultSelectWidet.OpenWidget();
        }
    }

    public void Update(){
        if(doingTutorial){
            return;
        }

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

    public void ReturnToMain(){
        ReturnToMainCoroutine().Start(this);
    }

    private IEnumerator ReturnToMainCoroutine(){
        resultSelectWidet.CloseWidget();
        yield return YieldInstructionCache.WaitSeconds(1.0f);
        stageSelectWidget.OpenWidget();

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

    public void LoadInGameScene(){
        if(currentSongInformation != null){
            SceneLoadCoroutine().Start(this);
        }
    }

    private IEnumerator SceneLoadCoroutine(){
        blackFadeImage.gameObject.SetActive(true);

        Tween fadeTween = blackFadeImage.DOFade(1.0f, 2.25f);
        yield return fadeTween.WaitForCompletion();

        SceneManager.LoadScene("01.InGameScene");
    }
}
