using DUMFUC.Input.Enums;
using DUMFUC.Utils;
using UnityEngine;
using UnityEngine.XR;
using DeviceType = DUMFUC.Input.Enums.DeviceType;
using Logger = BepInEx.Logging.Logger;

namespace DUMFUC.Input.Sources;

/// <summary>
/// Represents any digital input source from a VR controller (e.g., button press).
/// </summary>
public sealed class VrDigitalInputSource : VrInputSource
{
    private readonly InputFeatureUsage<bool> _featureUsage;
    
    public override InputType InputType => InputType.Digital;
    public override DeviceType DeviceType => Enums.DeviceType.XrController;
    
    public VrDigitalInputSource(VrInputType button, VrHand hand) : base(button, hand)
    {
        var feature = Action.GetDigitalFeatureUsage();
        if (feature == null)
            throw new ArgumentException($"Input type {button} is not a digital feature usage.");
        
        _featureUsage = feature;
    }

    public override bool GetBool() => InputDevices.TryGetFeatureValue_bool(InputDevice.deviceId, _featureUsage.name, out var value) && value;

    public override float GetFloat() => GetBool() ? 1f : 0f;
    public override Vector2 GetVector2() => Vector2.zero;
}