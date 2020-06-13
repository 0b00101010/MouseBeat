using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class NormalNode : Node
{
    private Tween moveTween;

    public override void Execute(Vector2 startPosition, Vector2 endPosition, int index){
        gameObject.SetActive(true);
        gameObject.transform.position = startPosition;
        
        positionIndex = index;

        InGameManager.instance.nodeInteractionController.AddActiveNormalNode(this, positionIndex);
        moveTween = gameObject.transform.DOMove(endPosition, defaultSpeed).SetEase(easeType);
    }

    public override void Interaction(){ 
        int judgeLevel = 0;
        switch(judgeLevel){
            case var k when judgePerfect == moveTween.position:
            judgeLevel = 4;
            break;

            case var k when moveTween.position > judgeGreat:
            judgeLevel = 3;
            break;
            
            case var k when moveTween.position > judgeGood:
            judgeLevel = 2;
            break;
            
            case var k when moveTween.position < judgeGood:
            judgeLevel = 1;
            break;
        }

        InGameManager.instance.nodeInteractionController.RemoveActiveNormalNode(this, positionIndex);
        InGameManager.instance.scoreManager.GetScore(judgeLevel, score);
        ObjectReset();
    }   

    public override void FailedInteraction(){
        InGameManager.instance.scoreManager.GetScore(0, 0);
        ObjectReset();
    }   

    public override void ObjectReset(){
        moveTween?.Kill();
        base.ObjectReset();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("JudgeLine")){
            FailedInteraction();
        }    
    }

}
