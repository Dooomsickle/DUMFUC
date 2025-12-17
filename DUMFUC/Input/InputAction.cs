using DUMFUC.Input.Enums;
using UnityEngine;

namespace DUMFUC.Input;

/// <summary>
/// Represents a VR controller input or key action.
/// </summary>
public class InputAction
{
     private readonly AInputSource _source;
     private InteractionState _state;
     
     /// <summary>
     /// Fires when an interaction starts.
     /// </summary>
     public Action<InputSnapshot> Started = _ => {};
     
     /// <summary>
     /// Fires each time an interaction is performed.
     /// </summary>
     public Action<InputSnapshot> Performed = _ => {};
     
     /// <summary>
     /// Fires when an interaction stops.
     /// </summary>
     public Action<InputSnapshot> Canceled = _ => {};
     
     /// <summary>
     /// The type of interaction this action uses (e.g. hold, double-tap, etc).
     /// </summary>
     public IInputInteraction Interaction { get; set; }
     
     /// <summary>
     /// The active state of the action. Disabled actions will not process input or fire events.
     /// </summary>
     public bool Enabled { get; set; } = true;
     
     public InputAction(AInputSource source, IInputInteraction interaction)
     {
          _source = source;
          Interaction = interaction;
     }

     /// <summary>
     /// The internal clock of the action. Should be called every frame.
     /// </summary>
     public void Tick()
     {
          if (!Enabled)
               return;
          
          var snapshot = new InputSnapshot
          {
               Type = _source.InputType,
               BoolValue = _source.GetBool(),
               FloatValue = _source.GetFloat(),
               Vector2Value = _source.GetVector2()
          };

          var hasInteractionOccurred = Interaction.Tick(snapshot, Time.unscaledTime, ref _state, out var phase);
          if (!hasInteractionOccurred)
               return;
          
          switch (phase)
          {
               case EActionPhase.Started:
                    Started.Invoke(snapshot);
                    break;
               case EActionPhase.Performed:
                    Performed.Invoke(snapshot);
                    break;
               case EActionPhase.Canceled:
                    Canceled.Invoke(snapshot);
                    break;
               default:
                    throw new ArgumentOutOfRangeException();
          }
     }
}