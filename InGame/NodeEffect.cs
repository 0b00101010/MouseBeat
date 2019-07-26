using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for(int i = 0; i < 5;i++)
        {
            gameObject.transform.localScale += new Vector3(0.1f, 0.1f,0.1f);
            yield return new WaitForSeconds(0.002f);
        }

        StartCoroutine(GameManager.instance.FadeOut(gameObject.GetComponent<SpriteRenderer>(), 0.2f));
        for(int i = 0; i < 5; i++) {
            gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0.1f);

            yield return new WaitForSeconds(0.05f);    
        }

        Destroy(gameObject);
    }
}
