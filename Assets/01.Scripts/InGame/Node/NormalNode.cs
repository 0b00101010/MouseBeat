using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class NormalNode : Node
{
    private Tween moveTween;

    public override void Execute(Vector2 startPosition, Vector2 endPosition){
        gameObject.SetActive(true);
        gameObject.transform.position = startPosition;

        moveTween = gameObject.transform.DOMove(endPosition, defaultSpeed).SetEase(easeType);
    }

    public override void Interaction(){ 
        int judgeLevel = 0;
        switch(judgeLevel){
            case var k when (judgePerfect - moveTween.position) < 0.03f:
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

        ObjectReset();
        InGameManager.instance.scoreManager.GetScore(judgeLevel, score);
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
