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
    private Image selectSquare;

    [SerializeField]
    private Image songImage;

    [Header("Resources")]
    [SerializeField]
    private Sprite[] selectSquareSprites;

    [SerializeField]
    private SongData songData;

    [Header("Values")]
    [SerializeField]
    private float duration;

    [Header("Event")]
    [SerializeField]
    private SongEvent executeEvent;

    private bool _isSelect;
    private bool isSelect{
        get{
            return _isSelect;
        }
        set{
            _isSelect = value;

            if(_isSelect){
                selectSquare.sprite = selectSquareSprites[1];
            } else {
                selectSquare.sprite = selectSquareSprites[0];
            }
        }
    }

    private void Awake(){
        songImage.sprite = songData.songImage;
    }

    public void Generate(){
        songImage.DOFade(1.0f, duration);
        selectSquare.DOFade(1.0f, duration);
    }

    public void Execute(){
        isSelect = true;

        GameManager.instance.NewGameStart();
        GameManager.instance.GameResult.songData = songData;
        GameManager.instance.SelectSong = songData.songFile;
        
        executeEvent.Invoke(songData);
    }

    public void Exit(){
        isSelect = false;
    }

}
