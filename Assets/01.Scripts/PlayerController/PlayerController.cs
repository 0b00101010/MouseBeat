using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKeyObserver
{
    private bool isHolding;

    private int leftValue;
    private int rightValue;

    private int leftHoldingValue;
    private int rightHoldingValue;

    private Dictionary<KeyCode,Action> keyDownActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode,Action> keyUpActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode,Action> keyHoldingActions = new Dictionary<KeyCode, Action>();

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

    private void Awake(){
        keyDownActions.Add(KeyCode.Mouse0, () => {
            leftEvent.Invoke(leftValue);
            leftHoldingValue = leftValue;
        });

        keyDownActions.Add(KeyCode.Mouse1, () => {
            rightEvent.Invoke(rightValue);
            rightHoldingValue = rightValue;
        });

        keyUpActions.Add(KeyCode.Mouse0, () => {
            leftUpEvent.Invoke(leftValue);
        });

        keyUpActions.Add(KeyCode.Mouse1, () => {
            rightUpEvent.Invoke(rightValue);
        });

        keyHoldingActions.Add(KeyCode.Mouse0, () => {
            if(leftValue.Equals(leftHoldingValue)){
                leftHoldingEvent.Invoke(leftHoldingValue);
            } else {
                leftUpEvent.Invoke(leftHoldingValue);
            }
        });

        keyHoldingActions.Add(KeyCode.Mouse1, () => {
            if(rightValue.Equals(rightHoldingValue)){
                rightHoldingEvent.Invoke(rightHoldingValue);
            } else {
                rightUpEvent.Invoke(rightHoldingValue);
            }
        });
    }

    private void Start(){
        MouseInputHandler.instance.AddObserver(this);
    }

    private void Update(){
        SetPosition();
        GetAdjacentLineValue();
    }

    public void KeyDownNotify(KeyCode key){
        if(keyDownActions.ContainsKey(key)){
            keyDownActions[key]();
        }
    }

    public void KeyHoldingNotify(KeyCode key){
        if(keyHoldingActions.ContainsKey(key)){
            keyHoldingActions[key]();
        }
    }

    public void KeyUpNotify(KeyCode key){
        if(keyUpActions.ContainsKey(key)){
            keyUpActions[key]();
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

    private void OnDestroy() {
        MouseInputHandler.instance.RemoveObserver(this);    
    }

}
