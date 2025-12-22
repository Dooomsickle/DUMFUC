using DUMFUC.Input.Enums;

namespace DUMFUC.Input.Interactions;

/// <summary>
/// Performs after holding the input for a specified duration.
/// </summary>
public sealed class HoldInteraction : IInputInteraction
{
    private readonly float _holdTime;
    private bool _performed;
    private readonly bool _continuous;
    
    public HoldInteraction(float holdTime, bool continuous = false)
    {
        _holdTime = holdTime;
        _performed = false;
        _continuous = continuous;
    }

    public bool Supports(InputType type) => true;

    public bool Tick(
        InputSnapshot snapshot,
        float time,
        ref InteractionState state,
        out ActionPhase phase
    )
    {
        phase = default;

        if (snapshot.BoolValue && !state.active)
        {
            state.active = true;
            state.startTime = time;
            phase = ActionPhase.Started;
            return true;
        }
        
        if (!_performed && snapshot.BoolValue && state.active && time - state.startTime >= _holdTime)
        {
            phase = ActionPhase.Performed;
            if (!_continuous)
                _performed = true;
            return true;
        }
        
        if (!snapshot.BoolValue && state.active)
        {
            phase = ActionPhase.Canceled;
            state.active = false;
            _performed = false;
            return true;
        }
        
        return false;
    }
}