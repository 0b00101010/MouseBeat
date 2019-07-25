using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float nextMapBpm;
    public float nextMapLastBeat;

    public static GameManager instance;

    public string nextMapName;

    public float NextMapBpm { get => nextMapBpm; set => nextMapBpm = value; }
    public float NextMapLastBeat { get => nextMapLastBeat; set => nextMapLastBeat = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        
    }


    public IEnumerator FadeIn(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator FadeOut(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeIn(Image image, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeOut(Image image, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

}
