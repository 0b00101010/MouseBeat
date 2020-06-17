using System.Linq;
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

    private bool isFailedInteraction;
    private bool isInteraction;

    private int judgeLevel;

    private new void Awake(){
        base.Awake();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        isFailedInteraction = false;
        isInteraction = false;
    }

    public override void Execute(Vector2 startPosition, Vector2 endPosition, int index){
        gameObject.SetActive(true);

        InGameManager.instance.nodeInteractionController.AddActiveLongNode(this, index);

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
        if(isFailedInteraction)
            return;

        if(isInteraction){
            if(judgeLevel.Equals(4)){
                InGameManager.instance.scoreManager.NodeEffect(positionIndex);
            }
            InGameManager.instance.scoreManager.GetScore(judgeLevel, score);
        }else{
            isInteraction = true;

            float progressPosition = 
            startPosition.Distance(gameObject.transform.position) / startPosition.Distance(endPosition);

            switch(progressPosition){
                case var k when judgePerfect - progressPosition < 0.1f:
                judgeLevel = 4;
                break;

                case var k when judgeGreat < progressPosition:
                judgeLevel = 3;
                break;

                case var k when judgeGood < progressPosition:
                judgeLevel = 2;
                break;

                case var k when judgeGood > progressPosition:
                judgeLevel = 1;
                break;
            }
        }
    }

    public void EndInteraction(){
        isFailedInteraction = true;
        ObjectOff();
    }
        
    public override void FailedInteraction(){
        InGameManager.instance.scoreManager.GetScore(0, 0);
        InGameManager.instance.nodeInteractionController.RemoveActiveLongNode(this, positionIndex);
    }

    public void TailStart(){
        TailCoroutine().Start(this);
    }

    private IEnumerator HeadCoroutine(){
        headTween = DOTween.To(() => headVector, value => headVector = value, endPosition, defaultSpeed);
        
        headTween.OnComplete(
            () => {
                if(!isInteraction){
                    ObjectOff();
                }
            }
        );
        
        while(true){
            if(headVector.Distance(endPosition) < 0.05f){
                gameObject.transform.position = headVector;
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
                break;
            }
            
            lineRenderer.SetPosition(1, tailVector);
            yield return YieldInstructionCache.WaitFrame;
        }

        ObjectReset();
    }

    public void ObjectOff(){
        isFailedInteraction = true;

        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.gray;
    }

    public override void ObjectReset(){
        this.enabled = false;
        isFailedInteraction = false;
        isInteraction = false;

        headTween?.Kill();
        headTween = null;

        tailTween?.Kill();
        tailTween = null;

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        InGameManager.instance.nodeInteractionController.RemoveActiveLongNode(this, positionIndex);

        gameObject.transform.position = Vector2.zero;
        base.ObjectReset();
    }
}
