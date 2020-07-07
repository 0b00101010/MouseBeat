using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameResult
{
    public SongData songData;
    public int score;
    public int[] judges = new int[5];
    public int totalJudgeCount;
    public float songProgress;
}
