using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public SoundManager soundManager;

    public int nextSongNumber;
    public float nextMapBpm;
    public int nextMapLastBeat;
    public string nextMapName;

    public int NextMapLastBeat { get => nextMapLastBeat; set => nextMapLastBeat = value; }
    public AudioClip[] GameMusics;
    public float NextMapBpm { get => nextMapBpm; set => nextMapBpm = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        soundManager = gameObject.GetComponent<SoundManager>();
        DontDestroyOnLoad(this.gameObject);
  
    }
    
    public IEnumerator FadeIn(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + (1.0f / repeatCount));
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator FadeOut(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (1.0f / repeatCount));
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeIn(Image image, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (1.0f / repeatCount));
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeOut(Image image, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (1.0f / repeatCount));
            yield return YieldInstructionCache.WaitingSeconds(spendTime / repeatCount);
        }
    }

}
