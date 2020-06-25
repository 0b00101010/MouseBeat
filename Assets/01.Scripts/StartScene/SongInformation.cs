using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SongInformation : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Image eyecatch;

    [SerializeField]
    private Image selectSquare;

    [SerializeField]
    private Image backgroundImage;

    [Header("Objects")]
    [SerializeField]
    private Sprite[] selectSquareSprites;

    private bool isSelect{
        get{
            return isSelect;
        }
        set{

        }
    };

}
