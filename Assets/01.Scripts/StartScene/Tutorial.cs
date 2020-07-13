using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField]
    private Sprite[] tutorialSprites;

    [Header("Objects")]
    [SerializeField]
    private Image tutorialImage;

    private bool isPlaying;

    public void ShowTutorials(){
        if(!isPlaying){
            StartSceneManager.DoingTutorial = true;
            TutorialCoroutine().Start(this);
        }
    }

    private IEnumerator TutorialCoroutine(){
        tutorialImage.gameObject.SetActive(true);

        for(int i = 0; i < tutorialSprites.Length; i++){
            tutorialImage.sprite = tutorialSprites[i];
            yield return new WaitUntil(() => Input.anyKey);
            yield return YieldInstructionCache.WaitSeconds(0.5f);
        }

        tutorialImage.gameObject.SetActive(false);
        StartSceneManager.DoingTutorial = false;
        yield return YieldInstructionCache.WaitFrame;
    }
}
