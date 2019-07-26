using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(StartGame());

    }

    private IEnumerator StartGame()
    {
        yield return StartCoroutine(GameManager.instance.FadeIn(GameObject.Find("Black").GetComponent<SpriteRenderer>(), 0.5f));
        SceneManager.LoadScene(1);

    }
}
