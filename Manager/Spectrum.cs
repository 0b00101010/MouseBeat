using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{      
    public List<GameObject> Sticks;        

    private void Update()
    {
        float[] SpectrumData = AudioListener.GetSpectrumData(2048, 0, FFTWindow.BlackmanHarris) ;         
        for (int i = 0; i < Sticks.Count; i++)
        {
            Vector2 FirstScale = Sticks[i].transform.localScale;                                  
            FirstScale.y = 3f + SpectrumData[i] * 500;                                         
            Sticks[i].transform.localScale = Vector2.MoveTowards(Sticks[i].transform.localScale, FirstScale, 0.1f);    
        }
    }

}
