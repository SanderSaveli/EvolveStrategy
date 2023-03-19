using System.Collections;
using UnityEngine;

public sealed class Coroutines : Singletone<Coroutines>
{
    public static Coroutine StartRoutine(IEnumerator corutine)
    {
        return instance.StartCoroutine(corutine);
    }
    public static void StopRoutine(IEnumerator corutine)
    {
        instance.StopCoroutine(corutine);
    }
    public static void StopRoutine(Coroutine corutine)
    {
        instance.StopCoroutine(corutine);
    }
}
