using DUMFUC.Input.Enums;
using DUMFUC.Input.Interactions;

namespace DUMFUC.Input;

/// <summary>
/// Defines the critera for an input action to fire.
/// </summary>
public interface IInputInteraction
{
    /// <summary>
    /// Whether this interaction supports the given input type.
    /// </summary>
    /// <param name="type">The type of input.</param>
    /// <returns><c>true</c> if supported, <c>false</c> otherwise.</returns>
    public bool Supports(EInputType type);

    /// <summary>
    /// The clock of the interaction.
    /// </summary>
    /// <param name="snapshot">A collection of the input source's current values.</param>
    /// <param name="time">The time of the invocation.</param>
    /// <param name="state">The internal state of the action.</param>
    /// <param name="phase">What phase (started, performed, canceled) the interaction is currently in.</param>
    /// <returns><c>true</c> if an interaction has occured this frame, <c>false</c> otherwise.</returns>
    public bool Tick(
        InputSnapshot snapshot,
        float time,
        ref InteractionState state,
        out EActionPhase phase
    );

    /// <summary>
    /// The default interaction (instantaneous press).
    /// </summary>
    public static IInputInteraction Default() => new HoldInteraction(0);
    
    /// <summary>
    /// Like the default, but fires every frame the input is held.
    /// </summary>
    public static IInputInteraction DefaultContinuous() => new HoldInteraction(0, true);
}