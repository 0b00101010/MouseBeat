using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StageWidget : UIWidget
{
    [Header("Position")]
    [SerializeField]
    private Transform textPosition;

    [SerializeField]
    private Transform underlinePosition;

    [SerializeField]
    private Transform previewPosition;

    [Header("Objects")]
    [SerializeField]
    private SongInformation[] songObjects;
    
    [SerializeField]
    private Image eyecatch;

    [SerializeField]
    private Text songNameText;

    [SerializeField]
    private Text composerNameText;

    [SerializeField]
    private Text bpmText;

    private Vector2 topPosition;
    private Vector2 bottomPosition;

    private bool isPreviewShow;

    private void Start(){
        topPosition = songObjects[0].gameObject.transform.position;
        bottomPosition = songObjects[songObjects.Length - 1].gameObject.transform.position;
    }

    private void Update(){
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll > 0.0f){
            if(songObjects[0].gameObject.transform.position.y > topPosition.y){
                SongItemMove(Vector2.down);
            }
        } else if(scroll < 0.0f){
            if(songObjects[songObjects.Length - 1].gameObject.transform.position.y < topPosition.y){
                SongItemMove(Vector2.up);
            }
        }
    }

    private void SongItemMove(Vector2 direction){
        for(int i = 0; i < songObjects.Length; i++){
            songObjects[i].gameObject.transform.Translate(direction / 5);
        }
    }

    public override void OpenWidget(){
        gameObject.SetActive(true);
        OpenCoroutine().Start(this);
    }   

    private IEnumerator OpenCoroutine(){
        Items[0].gameObject.transform.DOScaleY(1, Duration);
        WidgetTween = Items[0].DOFade(1, Duration);
        yield return WidgetTween.WaitForCompletion();

        Items[1].gameObject.transform.DOMove(textPosition.position, Duration);
        WidgetTween = Items[1].DOFade(1, Duration);

        Items[2].DOFade(1, Duration);

        yield return WidgetTween.WaitForCompletion();

        for(int i = 0; i < songObjects.Length; i++){
            songObjects[i].Generate();
        }
    }

    public void ShowPreview(SongData data){
        eyecatch.sprite = data.eyecatch;
        songNameText.text = data.songName;
        composerNameText.text = data.composerName;
        bpmText.text = "BPM : " + data.bpm.ToString();

        if(!isPreviewShow){
            eyecatch.DOFade(1.0f, 1.25f);
            songNameText.DOFade(1.0f, 1.25f);
            composerNameText.DOFade(1.0f, 1.25f);
            bpmText.DOFade(1.0f, 1.25f);

            eyecatch.transform.DOMoveX(previewPosition.position.x, 1.25f);
            songNameText.transform.DOMoveX(previewPosition.position.x, 1.25f);
            composerNameText.transform.DOMoveX(previewPosition.position.x, 1.25f);
            bpmText.transform.DOMoveX(previewPosition.position.x, 1.25f);

            isPreviewShow = true;
        }
    }
}
