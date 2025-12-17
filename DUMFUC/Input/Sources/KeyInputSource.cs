using DUMFUC.Input.Enums;
using UnityEngine;
using KeyCode = BepInEx.Unity.IL2CPP.UnityEngine.KeyCode;

namespace DUMFUC.Input.Sources;

public class KeyInputSource : AInputSource
{
    public override EInputDevice Device => EInputDevice.Keyboard;
    public override EInputType InputType => EInputType.Digital;
    
    public KeyCode Key { get; set; }
    
    public KeyInputSource(KeyCode key)
    {
        Key = key;
    }
    
    public override bool GetBool() => BepInEx.Unity.IL2CPP.UnityEngine.Input.GetKeyInt(Key);
    public override float GetFloat() => GetBool() ? 1f : 0f;
    public override Vector2 GetVector2() => Vector2.zero;
}