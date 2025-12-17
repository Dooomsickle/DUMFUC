using DUMFUC.Input.Enums;
using UnityEngine;

namespace DUMFUC.Input.Interactions;

/// <summary>
/// Performs when the input value changes (delta).
/// </summary>
public sealed class DeltaInteraction : IInputInteraction
{
    private object? _previousValue = null;

    public bool Supports(EInputType type) => true;

    public bool Tick(
        InputSnapshot snapshot,
        float time,
        ref InteractionState state,
        out EActionPhase phase
    )
    {
        phase = default;
        
        if (!snapshot.BoolValue && !state.active) // not active and not pressed
            return false;
        
        if (state.active && !snapshot.BoolValue) // released
        {
            state.active = false;
            phase = EActionPhase.Canceled;
            _previousValue = null;
            return true;
        }

        if (snapshot.BoolValue && _previousValue is null)
        {
            state.active = true;
            phase = EActionPhase.Started;
            _previousValue = snapshot.Type switch
            {
                EInputType.Digital => snapshot.BoolValue,
                EInputType.Axis1D => snapshot.FloatValue,
                EInputType.Axis2D => snapshot.Vector2Value,
                _ => null
            };
            return true;
        }
        
        var matches = PreviousValueMatches(snapshot);
        
        if (!matches && snapshot.BoolValue) // value changed
        {
            _previousValue = snapshot.Type switch
            {
                EInputType.Digital => snapshot.BoolValue,
                EInputType.Axis1D => snapshot.FloatValue,
                EInputType.Axis2D => snapshot.Vector2Value,
                _ => null
            };

            state.active = true;
            phase = EActionPhase.Performed;
            return true;
        }

        return false;
    }

    private bool PreviousValueMatches(InputSnapshot snapshot)
    {
        if (_previousValue is null)
            return false;
        
        switch (snapshot.Type)
        {
            case EInputType.Digital:
                var val = snapshot.BoolValue;
                return val == (bool)_previousValue;
            case EInputType.Axis1D:
                var fval = snapshot.FloatValue;
                return Mathf.Approximately(fval, (float)_previousValue);
            case EInputType.Axis2D:
                var v2val = snapshot.Vector2Value;
                return v2val == (Vector2)_previousValue;
        }
        
        return false;
    }
}