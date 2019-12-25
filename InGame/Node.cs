using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private float Coefficient;

    [SerializeField]
    private GameObject effect;

    protected Vector3 moveVector;

    private void FixedUpdate()
    {
        gameObject.transform.Translate(moveVector);
    }

    public virtual void Hit()
    {
        StageManager.instance.Score += (int)(Coefficient * (1 + StageManager.instance.Combo * 0.1f) * 100);
        if (!effect.Equals(null))
            Instantiate(effect, gameObject.transform.position, Quaternion.identity);

        if (Coefficient != 0)
            StageManager.instance.Combo++;

        if(Coefficient.Equals(1.0f))
            StageManager.instance.HitEffect(0);

        if (Coefficient.Equals(0.5f))
            StageManager.instance.HitEffect(1);

        if (Coefficient.Equals(0.0f))
            StageManager.instance.HitEffect(2);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CenterJudge")) {
            Coefficient = 1.0f;
        }
        if (collision.CompareTag("SideJudge")) {
            Coefficient = 0.5f;
        }
        if (collision.CompareTag("MissLine")) {
            Coefficient = 0.0f;
            StageManager.instance.Combo = 0;
        }
    }
}
