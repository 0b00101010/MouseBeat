using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLine : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private int leftIndex;

    [SerializeField]
    private int rightIndex;

    public int LeftIndex => leftIndex;
    public int RightIndex => rightIndex;
}
