using BepInEx.Logging;
using DUMFUC.Input.Enums;
using DUMFUC.Utils;
using UnityEngine.XR;

namespace DUMFUC.Input.Sources;

/// <summary>
/// A base class for inputs on VR controllers.
/// </summary>
public abstract class VrInputSource : InputSource
{
    protected InputDevice InputDevice;
    public override DeviceType DeviceType => DeviceType.XrController;
    
    /// <summary>
    /// The associated controller button or axis.
    /// </summary>
    public VrInputType Action { get; set; }
    
    /// <summary>
    /// The target hand.
    /// </summary>
    public VrHand Hand { get; set; }
    
    protected VrInputSource(VrInputType action, VrHand hand)
    {
        Action = action;
        Hand = hand;

        InputDevice = InputDevices.GetDeviceAtXRNode(Hand.ToXRNode());
        if (!InputDevice.isValid)
        {
            
        }
    }
}