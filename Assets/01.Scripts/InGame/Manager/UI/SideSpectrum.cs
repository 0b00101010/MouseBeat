using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideSpectrum : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Image[] sideSpectrums;

    private float[] audioData = new float[2048];

    private AudioSource audioSource;
    private float volume;
    private void Awake(){
        audioSource = gameObject.GetComponent<AudioSource>();
        volume = 1.0f - audioSource.volume;
    }

    private void Update(){
        AudioListener.GetSpectrumData(audioData, 0, FFTWindow.Rectangular);

        for(int i = 0; i < sideSpectrums.Length; i++){
            Vector2 firstSclae = sideSpectrums[i].gameObject.transform.localScale;
            firstSclae.y = ((audioData[i] * 800) * volume);
            sideSpectrums[i].gameObject.transform.localScale = Vector2.MoveTowards(sideSpectrums[i].gameObject.transform.localScale, firstSclae, 0.1f);
        }
    }
}
