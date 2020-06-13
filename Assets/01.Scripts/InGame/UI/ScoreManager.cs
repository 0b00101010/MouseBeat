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
    }
}
