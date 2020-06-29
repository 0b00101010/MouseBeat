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

    private bool isPreviewShow;

    public override void OpenWidget(){
        gameObject.SetActive(true);
        OpenCoroutine().Start(this);
    }   

    private IEnumerator OpenCoroutine(){
        Items[0].gameObject.transform.DOScaleY(1, Duration);
        WidgetTween = Items[0].DOFade(1, Duration);
        yield return WidgetTween.WaitForCompletion();

        Items[1].gameObject.transform.DOMove(textPosition.position, Duration);
        Items[2].gameObject.transform.DOMove(underlinePosition.position, Duration);
        
        Items[1].DOFade(1, Duration);
        WidgetTween = Items[2].DOFade(1, Duration);

        Items[3].DOFade(1, Duration);

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
