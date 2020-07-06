using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public static class ExtensionMethods{
    public static IEnumerator Start(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StartCoroutine(coroutine);
        return coroutine;
    }

    public static void Stop(this IEnumerator coroutine, MonoBehaviour behaivour){
        behaivour.StopCoroutine(coroutine);
    }

    public static void Log(this object value){
        Debug.Log(value.ToString());
    }

    public static int[] IndexOfMany(this string value, string findValue){
        List<int> indexs = new List<int>();

        for(int i = 0; i < value.Length; i++){
            if(value[i].Equals(findValue.First())){
                indexs.Add(i);
            }
        }

        return indexs.ToArray();
    }


    public static float Distance(this Vector2 positionA, Vector2 positionB){
        return Vector2.Distance(positionA, positionB);
    }

    public static string ToStringValue(this int value){
        switch(value){
            case var a when value < 10:
            return "00" + value.ToString();

            case var a when value < 100:
            return "0" + value.ToString();
            
            default:
            return value.ToString();
        }
    }
}