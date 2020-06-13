using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float defaultHP;
    private float hp;

    [ShowNonSerializedField]
    private int comboCount;
    
    [ShowNonSerializedField]
    private int score;
    

    [Header("Objects")]
    [SerializeField]
    private Image hpImage;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text comboText;

    [SerializeField]
    private Image judgeImage;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] judgeSprites;    

    [Header("Judge Effect")]
    [SerializeField]
    private float sizeUpValue;
    
    [SerializeField]
    private float duration;
    
    [SerializeField]
    private Ease easeType;

    [Header("Events")]
    [SerializeField]
    private VoidEvent deathEvent;

    private Tween judgeTween;
    private IEnumerator judgeCoroutine;

    private void Awake(){
        hp = defaultHP;
    }

    public void GetDamage(int damage){
        hp -= damage;

        if(hp <= 0){
            hp = 0;
            deathEvent.Invoke();
        }

        hpImage.fillAmount = hp / defaultHP;        
    }

    public void GetScore(int judge, int score){
        this.score += score;
        scoreText.text = this.score.ToString();

        switch(judge){
            case 4:
            judgeImage.sprite = judgeSprites[4];
            comboCount++;
            break;
            
            case 3:
            judgeImage.sprite = judgeSprites[3];
            comboCount++;
            break;
            
            case 2:
            judgeImage.sprite = judgeSprites[2];
            comboCount++;
            break;
            
            case 1:
            judgeImage.sprite = judgeSprites[1];
            comboCount++;
            break;
            
            case 0:
            judgeImage.sprite = judgeSprites[0];
            comboCount = 0;
            break;
        }

        if(comboCount < 100){
            comboText.text = comboCount.ToStringValue();
        }else{
            comboText.text = comboCount.ToString();
        } 

        judgeCoroutine?.Stop(this);
        judgeCoroutine = JudgeEvent().Start(this);
    }

    private IEnumerator JudgeEvent(){
        judgeTween?.Kill();

        judgeImage.transform.localScale = Vector3.one;
        judgeImage.color = Color.white;
        
        judgeImage.gameObject.SetActive(true);

        judgeTween = judgeImage.transform.DOScale(sizeUpValue, duration).SetEase(easeType);
        yield return judgeTween.WaitForCompletion();

        judgeTween.Kill();

        judgeTween = judgeImage.DOFade(0, 1.25f);
        yield return judgeTween.WaitForCompletion();

        judgeImage.gameObject.SetActive(false);

    }

    

}
