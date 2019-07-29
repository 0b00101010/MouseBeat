using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageButton : MonoBehaviour
{
    [SerializeField]
    private int songNumber;
    [SerializeField]
    private float mapBpm;
    [SerializeField]
    private int mapLastBeat;
    [SerializeField]
    private string mapName;

    public int SongNumber { get => songNumber; set => songNumber = value; }

    public void StartGame()
    {
        GameManager.instance.nextSongNumber = SongNumber;
        GameManager.instance.nextMapBpm = mapBpm;
        GameManager.instance.nextMapName = mapName;
        GameManager.instance.nextMapLastBeat = mapLastBeat;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return GameManager.instance.FadeIn(GameObject.Find("Black").GetComponent<SpriteRenderer>(),0.25f);
        SceneManager.LoadScene("02.InGame");
    }
}
