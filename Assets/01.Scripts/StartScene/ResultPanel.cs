using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultPanel : UIWidget
{
    [Header("Position")]
    [SerializeField]
    private Transform textPosition;

    [SerializeField]
    private Transform underlinePosition;

    [SerializeField]
    private Transform previewPosition;

    [Header("Preview")]
    [SerializeField]
    private Image eyecatch;

    [SerializeField]
    private Text songNameText;

    [SerializeField]
    private Text composerNameText;

    [SerializeField]
    private Text bpmText;

    [Header("Result")]
    [SerializeField]
    private Text[] judges;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text accuracyText;

    private GameResult result;

    private bool isPreviewShow;

    public override void OpenWidget(){
        gameObject.SetActive(true);

        result = GameManager.instance.GameResult;
        
        scoreText.text = result.score.ToString("D8");
        
        accuracyText.text = 
        ((result.judges[4] * 1.0f) + (result.judges[3] * 0.9f) 
        + (result.judges[2] * 0.7f) + (result.judges[1] * 0.2f)
        / (result.totalJudgeCount * 100.0f)).ToString("F2") + "%";

        for(int i = 0; i < judges.Length; i++){
            judges[i].text = judges[i].text + " " + result.judges[i].ToString();
        }

        OpenCoroutine().Start(this);
    }   

    private IEnumerator OpenCoroutine(){
        ShowPreview(result.songData);

        Items[0].gameObject.transform.DOScaleY(1, Duration);
        WidgetTween = Items[0].DOFade(1, Duration);
        yield return WidgetTween.WaitForCompletion();

        Items[1].gameObject.transform.DOMove(textPosition.position, Duration);
        WidgetTween = Items[1].DOFade(1, Duration);

        Items[2].DOFade(1, Duration);

        scoreText.DOFade(1, Duration);
        accuracyText.DOFade(1, Duration);

        for(int i = 0; i < judges.Length; i++){
            judges[i].DOFade(1, Duration);
        }

        yield return WidgetTween.WaitForCompletion();
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

    public override void CloseWidget(){
        CloseCoroutine().Start(this);
    }


    private IEnumerator CloseCoroutine(){
        ShowPreview(result.songData);

        Items[1].DOFade(0, Duration);
        WidgetTween = Items[2].DOFade(0, Duration);

        Items[3].DOFade(0, Duration);

        scoreText.DOFade(10, Duration);
        accuracyText.DOFade(0, Duration);

        for(int i = 0; i < judges.Length; i++){
            judges[i].DOFade(0, Duration);
        }

        Items[0].gameObject.transform.DOScaleY(0, Duration);
        WidgetTween = Items[0].DOFade(0, Duration);
        yield return WidgetTween.WaitForCompletion();
    }
}
