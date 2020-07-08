using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class NormalNode : Node
{
    private Tween moveTween = null;
    private Tween fadeTween = null;
    private Tween fallDownTween = null;

    private Vector2 startPosition;
    private Vector2 endPosition;

    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private float fallDownDuration;

    [SerializeField]
    private Ease fallDownEaes;

    public override void Execute(Vector2 startPosition, Vector2 endPosition, int index){
        gameObject.transform.position = startPosition;
        
        positionIndex = index;
        
        this.startPosition = startPosition;
        this.endPosition = endPosition;

        gameObject.SetActive(true);
        
        InGameManager.instance.nodeInteractionController.AddActiveNormalNode(this, positionIndex);
        moveTween = gameObject.transform.DOMove(endPosition, defaultSpeed).SetEase(easeType);
        
    }

    public override void Interaction(){ 
        int judgeLevel = 0;
        float position = startPosition.Distance(gameObject.transform.position)/startPosition.Distance(endPosition);
    
        if(position < 0.5f){
            return;
        }

        switch(judgeLevel){
            case var k when (judgePerfect - position) < 0.1f:
            judgeLevel = 4;
            break;

            case var k when position > judgeGreat:
            judgeLevel = 3;
            break;
            
            case var k when position > judgeGood:
            judgeLevel = 2;
            break;
            
            case var k when position < judgeGood:
            judgeLevel = 1;

            InGameManager.instance.nodeInteractionController.RemoveActiveNormalNode(this, positionIndex);
            InGameManager.instance.scoreManager.GetScore(judgeLevel, score);
            
            FailedInteractionEffect().Start(this);
            
            return;
        }

        InGameManager.instance.nodeInteractionController.RemoveActiveNormalNode(this, positionIndex);
        InGameManager.instance.scoreManager.GetScore(judgeLevel, score);
        
        InGameManager.instance.scoreManager.NodeEffect(positionIndex);

        ObjectReset();
    }   

    public override void FailedInteraction(){       
        InGameManager.instance.nodeInteractionController.RemoveActiveNormalNode(this, positionIndex);
        InGameManager.instance.scoreManager.GetScore(0, 0);
       
        FailedInteractionEffect().Start(this);
    }   

    private IEnumerator FailedInteractionEffect(){
        fadeTween = spriteRenderer.DOFade(0, fadeDuration);
        fallDownTween = gameObject.transform.DOMoveY(-8, fallDownDuration).SetEase(fallDownEaes);

        yield return fadeTween.WaitForCompletion();

        ObjectReset();
    }   

    public override void ObjectReset(){
        moveTween?.Kill();
        fadeTween?.Kill();
        fallDownTween?.Kill();

        spriteRenderer.color = Color.white;

        gameObject.transform.position = Vector2.zero;

        base.ObjectReset();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("JudgeLine")){
            // FailedInteraction();
            Interaction();
        }    
    }

}
