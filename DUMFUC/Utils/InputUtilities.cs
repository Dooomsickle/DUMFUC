using DUMFUC.Input.Enums;
using UnityEngine;
using UnityEngine.XR;
using KeyCode = BepInEx.Unity.IL2CPP.UnityEngine.KeyCode;

namespace DUMFUC.Utils;

public static class InputUtilities
{
    public static bool IsDigital(this EVrInputType vrInputType)
    {
        return vrInputType switch
        {
            EVrInputType.PrimaryButton or EVrInputType.SecondaryButton or EVrInputType.GripButton
                or EVrInputType.TriggerButton or EVrInputType.ThumbstickButton => true,
            _ => false
        };
    }

    public static bool IsAnalog(this EVrInputType vrInputType)
    {
        return vrInputType switch
        {
            EVrInputType.TriggerAxis or EVrInputType.GripAxis => true,
            _ => false
        };
    }
    
    public static bool IsAxis2D(this EVrInputType vrInputType)
    {
        return vrInputType switch
        {
            EVrInputType.ThumbstickAxis2D => true,
            _ => false
        };
    }
    
    public static InputFeatureUsage<bool>? GetDigitalFeatureUsage(this EVrInputType vrInputType)
    {
        if (!vrInputType.IsDigital()) return null;
        
        return vrInputType switch
        {
            EVrInputType.PrimaryButton => CommonUsages.primaryButton,
            EVrInputType.SecondaryButton => CommonUsages.secondaryButton,
            EVrInputType.GripButton => CommonUsages.gripButton,
            EVrInputType.TriggerButton => CommonUsages.triggerButton,
            EVrInputType.ThumbstickButton => CommonUsages.primary2DAxisClick,
            _ => null
        };
    }
    
    public static InputFeatureUsage<float>? GetAnalogFeatureUsage(this EVrInputType vrInputType)
    {
        if (!vrInputType.IsAnalog()) return null;
        
        return vrInputType switch
        {
            EVrInputType.TriggerAxis => CommonUsages.trigger,
            EVrInputType.GripAxis => CommonUsages.grip,
            _ => null
        };
    }
    
    public static InputFeatureUsage<Vector2>? GetAxis2DFeatureUsage(this EVrInputType vrInputType)
    {
        if (!vrInputType.IsAxis2D()) return null;
        
        return vrInputType switch
        {
            EVrInputType.ThumbstickAxis2D => CommonUsages.primary2DAxis,
            _ => null
        };
    }
    
    public static XRNode ToXRNode(this EVrHand hand) => hand switch
    {
        EVrHand.Left => XRNode.LeftHand,
        EVrHand.Right => XRNode.RightHand,
        _ => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
    };

    public static EInputType GetInputType(this EVrInputType vrButton) => vrButton switch
    {
        EVrInputType.PrimaryButton or EVrInputType.SecondaryButton or EVrInputType.GripButton
            or EVrInputType.TriggerButton
            or EVrInputType.ThumbstickButton => EInputType.Digital,
        EVrInputType.TriggerAxis or EVrInputType.GripAxis => EInputType.Axis1D,
        EVrInputType.ThumbstickAxis2D => EInputType.Axis2D,
        _ => throw new ArgumentOutOfRangeException(nameof(vrButton), vrButton, null)
    };
    
    public static EInputType GetInputType(this KeyCode key) => EInputType.Digital;

    public static bool IsInputTypeCompatible(EInputType given, EInputType expected)
    {
        // all binding types give a boolean value
        if (given == expected || expected == EInputType.Digital)
            return true;

        // digital values return a float of 0 or 1
        if (expected == EInputType.Axis1D && given == EInputType.Digital)
            return true;
        
        return false;
    }
}