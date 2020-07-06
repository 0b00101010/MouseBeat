using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "SongData", order = 0)]
public class SongData : ScriptableObject {
    public Sprite songImage;
    public Sprite eyecatch;

    public SongFile songFile;    

    public string songName;
    public string composerName;

    public float bpm;
}