using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int beat;
    private int score;

    [SerializeField]
    private float beatUpSpeed;

    private PatternRead patternRead;

    private void Start()
    {
        patternRead = gameObject.GetComponent<PatternRead>();
        patternRead.ReadFile("MapData/"+ GameManager.instance.nextMapName);
        beatUpSpeed = (60 / GameManager.instance.NextMapBpm / 4);
        StartCoroutine(BeatUp());
    }

    private IEnumerator BeatUp()
    {
        beat++;
        patternRead.CreateNode(beat);
        yield return new WaitForSeconds(beatUpSpeed);
        StartCoroutine(BeatUp());
    }



}
