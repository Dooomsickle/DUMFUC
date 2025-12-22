using DUMFUC.Input.Enums;
using UnityEngine;
using DeviceType = DUMFUC.Input.Enums.DeviceType;
using KeyCode = BepInEx.Unity.IL2CPP.UnityEngine.KeyCode;

namespace DUMFUC.Input.Sources;

public class KeyInputSource : InputSource
{
    public override DeviceType DeviceType => DeviceType.Keyboard;
    public override InputType InputType => InputType.Digital;
    
    public KeyCode Key { get; set; }
    
    public KeyInputSource(KeyCode key)
    {
        Key = key;
    }
    
    public override bool GetBool() => BepInEx.Unity.IL2CPP.UnityEngine.Input.GetKeyInt(Key);
    public override float GetFloat() => GetBool() ? 1f : 0f;
    public override Vector2 GetVector2() => Vector2.zero;
}