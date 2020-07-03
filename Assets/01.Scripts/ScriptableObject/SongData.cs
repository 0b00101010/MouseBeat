using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "SongData", order = 0)]
public class SongData : ScriptableObject {
    public Sprite eyecatch;
    
    public string songName;
    public string composerName;

    public float bpm;
}