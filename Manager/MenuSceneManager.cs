using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{

    [SerializeField]
    private StageButton[] stageButtons;
    private StageButton curButton;
    public StageButton CurButton {
        get => curButton;
        set
        {
            curButton = value;
            GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[curButton.SongNumber]);
            GameManager.instance.soundManager.MusicQueue();
        }
    }
    private void Start()
    {
        StartCoroutine(GameManager.instance.FadeOut(GameObject.Find("Black").GetComponent<SpriteRenderer>(),0.25f));
        CurButton = stageButtons[0];
    }
}
