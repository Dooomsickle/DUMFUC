using DUMFUC.Input.Enums;
using DUMFUC.Utils;
using UnityEngine;
using UnityEngine.XR;

namespace DUMFUC.Input.Sources;

/// <summary>
/// Represents a VR 2D axis input source (e.g. thumbstick).
/// </summary>
public sealed class Vr2dInputSource : AVrInputSource
{
    private readonly InputFeatureUsage<Vector2> _featureUsage;
    
    public override EInputType InputType => EInputType.Axis2D;
    
    public Vr2dInputSource(EVrInputType axis, EVrHand hand) : base(axis, hand)
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