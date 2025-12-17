using System.Collections;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace DUMFUC.Utils;

public class CoroutineRunner : MonoBehaviour
{
    static CoroutineRunner() => ClassInjector.RegisterTypeInIl2Cpp<CoroutineRunner>();
    
    private static CoroutineRunner? _instance;
    
    public static void Initialize()
    {
        if (_instance != null) return;
        
        var runnerObject = new GameObject("CoroutineRunner");
        _instance = runnerObject.AddComponent<CoroutineRunner>();
    }
    
    public static Coroutine Run(IEnumerator routine)
    {
        if (_instance == null)
            Initialize();
        
        return _instance!.StartCoroutine(routine.WrapToIl2Cpp());
    }
    
    public static void Stop(Coroutine coroutine)
    {
        if (_instance == null) return;
        
        _instance.StopCoroutine(coroutine);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}