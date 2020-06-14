using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class LongNode : Node
{   

    private LineRenderer lineRenderer;

    private Vector2 headVector;
    private Vector2 tailVector;

    private Vector2 startPosition;
    private Vector2 endPosition;

    private Tween headTween;
    private Tween tailTween;
    
    private new void Awake(){
        base.Awake();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    public override void Execute(Vector2 startPosition, Vector2 endPosition, int index){
        gameObject.SetActive(true);

        positionIndex = index;
        gameObject.transform.position = startPosition;

        this.startPosition = startPosition;
        this.endPosition = endPosition;

        this.enabled = true;

        headVector = startPosition;
        tailVector = startPosition;

        lineRenderer.SetPosition(0, headVector);
        lineRenderer.SetPosition(1, tailVector);


        HeadCoroutine().Start(this);

    }

    public override void Interaction(){

    }

    public override void FailedInteraction(){

    }

    public void TailStart(){
        TailCoroutine().Start(this);
    }

    private IEnumerator HeadCoroutine(){
        headTween = DOTween.To(() => headVector, value => headVector = value, endPosition, defaultSpeed);
        
        while(true){
            if(headVector.Distance(endPosition) < 0.05f){
                break;
            }

            lineRenderer.SetPosition(0, headVector);
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private IEnumerator TailCoroutine(){
        tailTween = DOTween.To(() => tailVector, value => tailVector = value, endPosition, defaultSpeed);
        
        while(true){
            if(tailVector.Distance(endPosition) < 0.05f){
                gameObject.transform.name.Log();
                break;
            }
            
            lineRenderer.SetPosition(1, tailVector);
            yield return YieldInstructionCache.WaitFrame;
        }

        ObjectReset();
    }

    public override void ObjectReset(){
        this.enabled = false;

        headTween?.Kill();
        headTween = null;

        tailTween?.Kill();
        tailTween = null;

        base.ObjectReset();
    }

}
