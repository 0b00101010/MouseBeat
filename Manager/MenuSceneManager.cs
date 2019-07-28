using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{

    [SerializeField]
    private StageButton[] stageButtons;

    private int index = 0;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && index > 0) {
            index--;
        }

        else if (Input.GetKeyDown(KeyCode.D) && index < stageButtons.Length) {
            index++;
        }
    }
    private void Start()
    {
        StartCoroutine(GameManager.instance.FadeOut(GameObject.Find("Black").GetComponent<SpriteRenderer>(),0.25f));
        CurButton = stageButtons[0];
    }
}
