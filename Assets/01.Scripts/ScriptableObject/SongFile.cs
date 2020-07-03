using UnityEngine;

[CreateAssetMenu(fileName = "SongFile", menuName = "SongFile", order = 0)]
public class SongFile : ScriptableObject {
    public AudioClip audioClip;
    public TextAsset mapTextAsset;
}