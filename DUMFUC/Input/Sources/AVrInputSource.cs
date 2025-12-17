using BepInEx.Logging;
using DUMFUC.Input.Enums;
using DUMFUC.Utils;
using UnityEngine.XR;

namespace DUMFUC.Input.Sources;

/// <summary>
/// A base class for inputs on VR controllers.
/// </summary>
public abstract class AVrInputSource : AInputSource
{
    protected InputDevice InputDevice;
    public override EInputDevice Device => EInputDevice.XrController;
    
    /// <summary>
    /// The associated controller button or axis.
    /// </summary>
    public EVrInputType Action { get; set; }
    
    /// <summary>
    /// The target hand.
    /// </summary>
    public EVrHand Hand { get; set; }
    
    protected AVrInputSource(EVrInputType action, EVrHand hand)
    {
        Action = action;
        Hand = hand;

        InputDevice = InputDevices.GetDeviceAtXRNode(Hand.ToXRNode());
        if (!InputDevice.isValid)
        {
            
        }
    }
}