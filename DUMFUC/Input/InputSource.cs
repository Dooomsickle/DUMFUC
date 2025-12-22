using DUMFUC.Input.Enums;
using UnityEngine;
using DeviceType = DUMFUC.Input.Enums.DeviceType;

namespace DUMFUC.Input;

/// <summary>
/// Base class for input sources such as keyboard, mouse, or XR controllers.
/// </summary>
public abstract class InputSource
{
    /// <summary>
    /// The type of data this input source provides.
    /// </summary>
    public abstract InputType InputType { get; }
    
    /// <summary>
    /// The device this input source is associated with.
    /// </summary>
    public abstract DeviceType DeviceType { get; }

    /// <summary>
    /// Gets the boolean value of the input source. This can vary based on the input type:
    /// <list type="bullet">
    /// <item>Digital: true if pressed, false otherwise.</item>
    /// <item>Axis1D: true if the float value is greater than its deadzone, false otherwise.</item>
    /// <item>Axis2D: true if the vector is not zero, false otherwise.</item>
    /// </list>
    /// Custom input sources can give their own interpretation of the boolean value.
    /// </summary>
    public abstract bool GetBool();
    
    /// <summary>
    /// Gets the float value of the input source. For non-analog inputs, this typically returns <c>1f</c> when on and vice versa.
    /// </summary>
    public abstract float GetFloat();
    
    /// <summary>
    /// Gets the Vector2 value of the input source. Typically only applicable to 2D axis inputs, such as a joystick.
    /// </summary>
    public abstract Vector2 GetVector2();
}