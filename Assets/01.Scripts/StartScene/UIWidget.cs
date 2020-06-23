using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIWidget : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Image[] items;

    [Header("Values")]
    [SerializeField]
    private float duration;

    private Tween widgetTween;

    public void OpenWidget(){
        gameObject.SetActive(true);

        for(int i = 0; i < items.Length; i++){
            if(i.Equals(items.Length - 1)){
                widgetTween = items[i].DOFade(1, duration);
            } else {
                items[i].DOFade(1, duration);
            }
        }
    }

    public void OpenWidget(Action callback = null){
        gameObject.SetActive(true);

        for(int i = 0; i < items.Length; i++){
            if(i.Equals(items.Length - 1)){
                widgetTween = items[i].DOFade(1, duration);
            } else {
                items[i].DOFade(1, duration);
            }
        }

        widgetTween.OnComplete(() => {
            callback();
        });
    }

    public void CloseWidget(){
        for(int i = 0; i < items.Length; i++){
            if(i.Equals(items.Length - 1)){
                widgetTween = items[i].DOFade(0, duration);
            } else {
                items[i].DOFade(0, duration);
            }
        }

        widgetTween.OnComplete(() => {
            gameObject.SetActive(false);
        });
    }    
    
    public void CloseWidget(Action callback = null){
        for(int i = 0; i < items.Length; i++){
            if(i.Equals(items.Length - 1)){
                widgetTween = items[i].DOFade(0, duration);
            } else {
                items[i].DOFade(0, duration);
            }
        }

        widgetTween.OnComplete(() => {
            gameObject.SetActive(false);
            callback();
        });
    }
}
