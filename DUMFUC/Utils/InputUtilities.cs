using DUMFUC.Input.Enums;
using UnityEngine;
using UnityEngine.XR;
using KeyCode = BepInEx.Unity.IL2CPP.UnityEngine.KeyCode;

namespace DUMFUC.Utils;

public static class InputUtilities
{
    public static bool IsDigital(this VrInputType vrInputType)
    {
        return vrInputType switch
        {
            VrInputType.PrimaryButton or VrInputType.SecondaryButton or VrInputType.GripButton
                or VrInputType.TriggerButton or VrInputType.ThumbstickButton => true,
            _ => false
        };
    }

    public static bool IsAnalog(this VrInputType vrInputType)
    {
        return vrInputType switch
        {
            VrInputType.TriggerAxis or VrInputType.GripAxis => true,
            _ => false
        };
    }
    
    public static bool IsAxis2D(this VrInputType vrInputType)
    {
        return vrInputType switch
        {
            VrInputType.ThumbstickAxis2D => true,
            _ => false
        };
    }
    
    public static InputFeatureUsage<bool>? GetDigitalFeatureUsage(this VrInputType vrInputType)
    {
        if (!vrInputType.IsDigital()) return null;
        
        return vrInputType switch
        {
            VrInputType.PrimaryButton => CommonUsages.primaryButton,
            VrInputType.SecondaryButton => CommonUsages.secondaryButton,
            VrInputType.GripButton => CommonUsages.gripButton,
            VrInputType.TriggerButton => CommonUsages.triggerButton,
            VrInputType.ThumbstickButton => CommonUsages.primary2DAxisClick,
            _ => null
        };
    }
    
    public static InputFeatureUsage<float>? GetAnalogFeatureUsage(this VrInputType vrInputType)
    {
        if (!vrInputType.IsAnalog()) return null;
        
        return vrInputType switch
        {
            VrInputType.TriggerAxis => CommonUsages.trigger,
            VrInputType.GripAxis => CommonUsages.grip,
            _ => null
        };
    }
    
    public static InputFeatureUsage<Vector2>? GetAxis2DFeatureUsage(this VrInputType vrInputType)
    {
        if (!vrInputType.IsAxis2D()) return null;
        
        return vrInputType switch
        {
            VrInputType.ThumbstickAxis2D => CommonUsages.primary2DAxis,
            _ => null
        };
    }
    
    public static XRNode ToXRNode(this VrHand hand) => hand switch
    {
        VrHand.Left => XRNode.LeftHand,
        VrHand.Right => XRNode.RightHand,
        _ => throw new ArgumentOutOfRangeException(nameof(hand), hand, null)
    };

    public static InputType GetInputType(this VrInputType vrButton) => vrButton switch
    {
        VrInputType.PrimaryButton or VrInputType.SecondaryButton or VrInputType.GripButton
            or VrInputType.TriggerButton
            or VrInputType.ThumbstickButton => InputType.Digital,
        VrInputType.TriggerAxis or VrInputType.GripAxis => InputType.Axis1D,
        VrInputType.ThumbstickAxis2D => InputType.Axis2D,
        _ => throw new ArgumentOutOfRangeException(nameof(vrButton), vrButton, null)
    };
    
    public static InputType GetInputType(this KeyCode key) => InputType.Digital;

    public static bool IsInputTypeCompatible(InputType given, InputType expected)
    {
        // all binding types give a boolean value
        if (given == expected || expected == InputType.Digital)
            return true;

        // digital values return a float of 0 or 1
        if (expected == InputType.Axis1D && given == InputType.Digital)
            return true;
        
        return false;
    }
}