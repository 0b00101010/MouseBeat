using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject leftArea;

    [SerializeField]
    private GameObject rightArea;

    [SerializeField]
    private RectTransform mouseCursor;

    private RectTransform rectTrasform;

    private Vector2 mousePos;

    [SerializeField]
    private Animator anim;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        rectTrasform = gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if(!(mousePos.x > 5.6f || mousePos.x < -5.6f || mousePos.y < -5 || mousePos.y > 5))
            mouseCursor.position = mousePos;
        
        mousePos.y = rectTrasform.position.y;

        if (!(mousePos.x > 5.6f || mousePos.x < -5.6f))
            rectTrasform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isTrigger"); 
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("isTrigger");
        }
    }
}
