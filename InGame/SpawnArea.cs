using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public List<Node> nodeList = new List<Node>();

    public void Update()
    {
        if (nodeList.Count > 0)
        {
           
            if (nodeList[0].transform.position.y < -4)
            {
                StageManager.instance.HitEffect(2);
                Destroy(nodeList[0].gameObject,0.02f);
                nodeList.Remove(nodeList[0]);
            }
        }
    }

    public void Hit()
    {
        if (nodeList.Count > 0)
        {
            nodeList[0].Hit();
            nodeList.Remove(nodeList[0]);
        }
    }
}
