using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private LineRenderer backgroundRenderer;

    private Color topColor;
    private Color bottomColor;

    private IEnumerator executeCoroutine;

    private void Awake(){
        topColor = backgroundRenderer.startColor;
        bottomColor = backgroundRenderer.endColor;
    }

    public void Execute(){
        executeCoroutine?.Stop(this);
        executeCoroutine = ExecuteCoroutine().Start(this);
    }

    private IEnumerator ExecuteCoroutine(){
        Color beforeTopColor = topColor;
        Color beforeBottomColor = bottomColor;

        topColor = RandomColor();
        bottomColor = RandomColor();

        for(int i = 0; i < 60; i++){
            backgroundRenderer.startColor = Color.Lerp(beforeTopColor, topColor, i / 60.0f);
            backgroundRenderer.endColor = Color.Lerp(beforeBottomColor, bottomColor, i / 60.0f);

            yield return YieldInstructionCache.WaitSeconds(0.05f);
        }
    }

    private Color RandomColor(){
        return Color.HSVToRGB(Random.value, 1, 1);
    }

}
