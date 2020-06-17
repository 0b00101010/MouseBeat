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

    [Space(30)]
    [SerializeField]
    private IntEvent leftUpEvent;

    [SerializeField]
    private IntEvent rightUpEvent;

    [Space(30)]
    [SerializeField]
    private IntEvent leftHoldingEvent;
    
    [SerializeField]
    private IntEvent rightHoldingEvent;

    private void Update(){
        SetPosition();
        GetAdjacentLineValue();

        switch(Input.anyKey){
            case var k when Input.GetKeyDown(KeyCode.Z)||Input.GetMouseButtonDown(0):
            leftEvent.Invoke(leftValue);
            break; 

            case var k when Input.GetKeyDown(KeyCode.X)||Input.GetMouseButtonDown(1):
            rightEvent.Invoke(rightValue);   
            break;

            case var k when Input.GetKeyUp(KeyCode.Z)||Input.GetMouseButtonUp(0):
            leftUpEvent.Invoke(leftValue);
            break; 

            case var k when Input.GetKeyUp(KeyCode.X)||Input.GetMouseButtonUp(1):
            rightUpEvent.Invoke(rightValue);   
            break;

            case var k when Input.GetKey(KeyCode.Z) || Input.GetMouseButton(0):
            leftHoldingEvent.Invoke(leftValue);
            break;

            case var k when Input.GetKey(KeyCode.X) || Input.GetMouseButton(1):
            rightHoldingEvent.Invoke(rightValue);
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
