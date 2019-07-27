using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCtrl : MonoBehaviour
{
    [SerializeField]
    private SpawnArea leftArea;

    [SerializeField]
    private SpawnArea rightArea;

    [SerializeField]
    private RectTransform mouseCursor;

    private RectTransform rectTrasform;

    private Vector2 mousePos;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject[] lines;

    private float recentLineDistance = 100f;
    private Line recentLine;

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

       
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("isTrigger");
            leftArea.Hit();
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("isTrigger");
            rightArea.Hit();
        }
    }

    private void LateUpdate()
    {
        var query = from i in lines
                    orderby (gameObject.transform.position - i.transform.position).sqrMagnitude
                    select i;

        recentLine = query.First().GetComponent<Line>();
        leftArea = recentLine.GetLeft();
        rightArea = recentLine.GetRight();
    }
}
