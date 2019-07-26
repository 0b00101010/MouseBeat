using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNode : Node
{
    [SerializeField]
    private GameObject longNodeHead;

    [SerializeField]
    private GameObject longNodeBody;

    [SerializeField]
    private GameObject longNodeTail;

    private GameObject prevBody;

    private bool isRetry;

    public SpawnArea area;

    public void LongNodeStart() {
        GameObject head = Instantiate(longNodeHead,gameObject.transform.position,Quaternion.identity);
        prevBody = head;
        area.nodeList.Add(prevBody.GetComponent<Node>());
        isRetry = true;
    }

    private void Update()
    {
        if (!isRetry)
            return;

        if (prevBody.transform.position.y < 2.6f)
        {
            prevBody = Instantiate(longNodeBody, gameObject.transform.position, Quaternion.identity);
        }
        
    }
    

    public void LongNodeEnd() {
        isRetry = false;
        Vector3 vec = gameObject.transform.position;
        vec.y = prevBody.transform.position.y + 2.4f;
        GameObject tail = Instantiate(longNodeTail,vec,Quaternion.identity);
        area.nodeList.Add(tail.GetComponent<Node>());
        Destroy(gameObject);
    }

}
