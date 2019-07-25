using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector2(0f,-1.25f));
    }

    public void Hit()
    {
    }

    private void OnDestroy()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterJudge")) { }
        if (collision.CompareTag("SideJudge")) { }
        if (collision.CompareTag("MissLine")) {
           
        }
    }
}
