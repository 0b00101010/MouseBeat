using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TouchToStart : MonoBehaviour
{
    private Image touchToStart;
    private Tween fadeTween;
    private IEnumerator fadeCoroutine;

    private void Awake(){
        touchToStart = gameObject.GetComponent<Image>();
    }

    private void Start(){
        fadeCoroutine = FadeCoroutine().Start(this);
    }

    private IEnumerator FadeCoroutine(){
        while(true){
            fadeTween = touchToStart.DOFade(0, 1.25f);
            yield return fadeTween.WaitForCompletion();
            
            fadeTween = touchToStart.DOFade(1, 1.25f);
            yield return fadeTween.WaitForCompletion();
        }
    }

    private void OnDisable() {
        fadeCoroutine.Stop(this);
    }
}
