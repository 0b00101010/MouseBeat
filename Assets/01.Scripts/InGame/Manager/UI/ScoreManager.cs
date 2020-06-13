using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

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
    

    [Header("Events")]
    [SerializeField]
    private VoidEvent deathEvent;

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
    }
}
