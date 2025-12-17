using DUMFUC.Input.Enums;
using DUMFUC.Utils;
using UnityEngine;
using UnityEngine.XR;

namespace DUMFUC.Input.Sources;

/// <summary>
/// Represents a VR analog input source (e.g., trigger or grip).
/// </summary>
public sealed class VrAnalogInputSource : AVrInputSource
{
    private readonly InputFeatureUsage<float> _featureUsage;
    
    public override EInputType InputType => EInputType.Axis1D;
    
    public float Deadzone { get; set; } = 0f;

    public VrAnalogInputSource(EVrInputType axis, EVrHand hand) : base(axis, hand)
    {
        var feature = axis.GetAnalogFeatureUsage();
        if (feature == null)
            throw new ArgumentException($"Input type {axis} is not an analog feature usage.");

        _featureUsage = feature;
    }

    public override bool GetBool() => GetFloat() > Deadzone;
    public override float GetFloat() => InputDevices.TryGetFeatureValue_float(InputDevice.deviceId, _featureUsage.name, out var value) && value > Deadzone ? value : 0f;
    public override Vector2 GetVector2() => Vector2.zero;
}