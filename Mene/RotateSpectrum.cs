using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpectrum : MonoBehaviour
{
    public List<GameObject> Sticks;

    [SerializeField]
    private GameObject block;

    [SerializeField]
    private float interval;

    [SerializeField]
    private int count;

    [SerializeField ]
    private float radius;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            float x = radius * Mathf.Cos(interval * i);
            float y = radius * Mathf.Sin(interval * i);
            Vector2 newPos = new Vector2(x, y);

            
            GameObject newObject = Instantiate(block, newPos, Quaternion.identity);
            Vector3 rotatePos = new Vector3(0, 0, i / 360);
            newObject.transform.rotation = Quaternion.LookRotation(Vector3.forward,newObject.transform.position);
            newObject.transform.parent = gameObject.transform;
            Sticks.Add(newObject);
            
        }
    }

    private void Update()
    {
        float[] SpectrumData = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < Sticks.Count; i++)
        {
            Sticks[i].transform.localScale = new Vector3(50, 15 + SpectrumData[i] * 2500, 1);
        }
    }
}
