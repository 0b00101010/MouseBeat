using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCircle : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float rotateAngle;

    private void Awake(){
        RotateCoroutine().Start(this);
    }

    private IEnumerator RotateCoroutine(){
        while(true){
            gameObject.transform.Rotate(Vector3.forward * rotateAngle);
            yield return YieldInstructionCache.WaitFrame;
        }
    }
}
