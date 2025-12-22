using DUMFUC.Input.Enums;
using DUMFUC.Utils;
using UnityEngine;
using UnityEngine.XR;

namespace DUMFUC.Input.Sources;

/// <summary>
/// Represents a VR 2D axis input source (e.g. thumbstick).
/// </summary>
public sealed class Vr2dInputSource : VrInputSource
{
    private readonly InputFeatureUsage<Vector2> _featureUsage;
    
    public override InputType InputType => InputType.Axis2D;
    
    public Vr2dInputSource(VrInputType axis, VrHand hand) : base(axis, hand)
    {
        var feature = axis.GetAxis2DFeatureUsage();
        if (feature == null)
            throw new ArgumentException($"Input type {axis} is not a 2D axis feature usage.");
        
        _featureUsage = feature;
    }
    
    public override bool GetBool() => GetVector2() != Vector2.zero;
    public override float GetFloat() => GetVector2().y;
    public override Vector2 GetVector2() => InputDevices.TryGetFeatureValue_Vector2f(InputDevice.deviceId, _featureUsage.name, out var value) ? value : Vector2.zero;
}