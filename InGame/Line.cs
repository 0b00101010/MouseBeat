using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private SpawnArea leftSpawnArea;

    [SerializeField]
    private SpawnArea rightSpawnArea;

    public SpawnArea GetLeft()
    {
        return leftSpawnArea;
    }

    public SpawnArea GetRight()
    {
        return rightSpawnArea;
    }
}
