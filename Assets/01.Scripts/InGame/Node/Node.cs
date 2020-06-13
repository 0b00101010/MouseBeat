using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private int _score;

    [SerializeField]
    private int _reducedHp;

    [SerializeField]
    private float _defaultSpeed;

    [SerializeField]
    private float _judgePerfect;
    
    [SerializeField]
    private float _judgeGreat;
    
    [SerializeField]
    private float _judgeGood;

    private SpriteRenderer _spriteRenderer;

    protected int score => _score;
    protected int reducedHp => _reducedHp;
    protected float defaultSpeed => _defaultSpeed;    
    
    protected float judgePerfect => _judgePerfect;
    protected float judgeGreat => _judgeGreat;
    protected float judgeGood => _judgeGood;
    
    protected SpriteRenderer spriteRenderer => _spriteRenderer;
    
    private void Awake(){
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Execute(Vector2 startPosition, Vector2 endPosition){ }

    public virtual void Interaction(){ }
    
    public virtual void FailedInteraction(){}

    public virtual void ObjectReset(){ 
        gameObject.SetActive(false);
    }   
}
