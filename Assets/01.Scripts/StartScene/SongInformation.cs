using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode]
public class SongInformation : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Image eyecatch;

    [SerializeField]
    private Image selectSquare;

    [SerializeField]
    private Image backgroundImage;

    [Space(10)]
    [SerializeField]
    private Image songNameBackground;

    [SerializeField]
    private Text songNameText;
    
    [Space(10)]
    [SerializeField]
    private Image composerNameBackground;

    [SerializeField]
    private Text composerNameText;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] selectSquareSprites;

    [SerializeField]
    private SongData songData;

    [Header("Values")]
    [SerializeField]
    private float duration;

    private bool isSelect{
        get{
            return isSelect;
        }
        set{
            if(isSelect){
                selectSquare.sprite = selectSquareSprites[1];
            } else {
                selectSquare.sprite = selectSquareSprites[0];
            }
        }
    }

    private void Awake(){
        eyecatch.sprite = songData.eyecatch;
        songNameText.text = songData.songName;
        composerNameText.text = songData.composerName;
    }

    public void Generate(){
        eyecatch.DOFade(1.0f, duration);
        selectSquare.DOFade(1.0f, duration);

        backgroundImage.DOFade(1.0f, duration);

        songNameBackground.material.DOFade(1.0f, duration);
        songNameText.DOFade(1.0f, duration);

        composerNameBackground.DOFade(1.0f, duration);
        composerNameText.DOFade(1.0f, duration);

    }

}
