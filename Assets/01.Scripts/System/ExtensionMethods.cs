using System.Diagnostics;
using System.Security.AccessControl;
using UnityEngine;
using System.Collections;
using Debug = UnityEngine.Debug;

public static class ExtensionMethods{
    public static IEnumerator Start(this IEnumerator coroutine, MonoBehaviour behaviour){
        behaviour.StartCoroutine(coroutine);
        return coroutine;
    }

    public static void Stop(this IEnumerator coroutine, MonoBehaviour behaivour){
        behaivour.StopCoroutine(coroutine);
    }

    public static void Log(string value){
        Debug.Log(value);
    }
}