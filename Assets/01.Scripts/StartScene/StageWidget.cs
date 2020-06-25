using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StageWidget : UIWidget
{
    [Header("Position")]
    [SerializeField]
    private Transform textPosition;

    [SerializeField]
    private Transform underlinePosition;

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
        Items[2].DOFade(1, Duration);
    }
}
