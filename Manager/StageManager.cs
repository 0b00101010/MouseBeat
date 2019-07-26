using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
{
    private int beat;
    private int score;

    private int combo;

    [SerializeField]
    private ValueCtrl scoreViewCtrl;

    public int Score {
        get => score;
        set {
            score = value;
            scoreViewCtrl.UpdateShame(score);
        }
    }

    public Image hitEffect;
    public Sprite[] hitEffectSprites;

    public int Combo { get => combo; set => combo = value; }

    public static StageManager instance;
    
    public float beatUpSpeed;
    
    private PatternRead patternRead;

    
    public Sprite[] backgorunds;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[GameManager.instance.nextSongNumber]);
        GameManager.instance.soundManager.MusicQueue();

        patternRead = gameObject.GetComponent<PatternRead>();
        beatUpSpeed = (60 / GameManager.instance.NextMapBpm / 4);
        patternRead.ReadFile("MapData/" + GameManager.instance.nextMapName);

    }

    private void Start()
    {
        StartCoroutine(BeatUp());
    }


    private IEnumerator BeatUp()
    {
        beat++;
        patternRead.CreateNode(beat);
        yield return new WaitForSeconds(beatUpSpeed);
        StartCoroutine(BeatUp());
    }


    public void HitEffect(int effectNum)
    {
        StartCoroutine(effect(effectNum));
    }

    private IEnumerator effect(int effectNum)
    {
        hitEffect.sprite = hitEffectSprites[effectNum];
        yield return StartCoroutine(GameManager.instance.IFadeIn(hitEffect,0.02f));
        yield return StartCoroutine(GameManager.instance.IFadeOut(hitEffect,0.3f));
    }
}


