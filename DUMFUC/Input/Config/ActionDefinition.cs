using BepInEx.Unity.IL2CPP.UnityEngine;
using DUMFUC.Input.Enums;

namespace DUMFUC.Input.Config;

[Serializable]
public class ActionDefinition
{
    public string name = "Unnamed Action";
    
    public InputType inputType_DONOTCHANGE;
    public DeviceType deviceType;
    
    public InteractionDefinition interaction = null!;
    
    // XR Controller Specific
    public VrBindingData[] vrBindings = Array.Empty<VrBindingData>();
    
    // Keyboard
    public KeyCode[] keyBindings = Array.Empty<KeyCode>();
}