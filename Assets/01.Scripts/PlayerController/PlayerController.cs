using System.ComponentModel;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int leftValue;
    private int rightValue;

    [Header("Objects")]
    [SerializeField]
    private VerticalLine[] verticalLines;

    [Header("Events")]
    [SerializeField]
    private IntEvent leftEvent;

    [SerializeField]
    private IntEvent rightEvent;

    private void Update(){
        SetPosition();
        GetAdjacentLineValue();

        switch(Input.anyKeyDown){
            case var k when Input.GetKeyDown(KeyCode.Z)||Input.GetMouseButtonDown(0):
            leftEvent.Invoke(leftValue);
            break; 

            case var k when Input.GetKeyDown(KeyCode.X)||Input.GetMouseButtonDown(1):
            rightEvent.Invoke(rightValue);   
            break;
        }
    }

    private void SetPosition(){
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = newPosition;
    }

    private void GetAdjacentLineValue(){
        var query = from o in verticalLines
                orderby (gameObject.transform.position - o.gameObject.transform.position).sqrMagnitude
                select o;

        VerticalLine selectObject = query.FirstOrDefault();

        leftValue = selectObject.LeftIndex;
        rightValue = selectObject.RightIndex;
    }

}
