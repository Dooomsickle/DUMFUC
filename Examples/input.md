# Input Action Examples

## Defining an action programatically
```csharp
var jumpAction = new InputAction(
    new VrDigitalInputSource(EVrInputType.SecondaryButton, EVrHand.Right), // vr input based on oculus touch controllers
    IInputInteraction.Default() // fires on value
    );
```

## Consuming an action
```csharp
moveAction.Performed += OnMove; // EVrInputType.ThumbstickAxis2D
moveAction.Canceled += OnStopMove;

void OnMove(InputSnapshot snapshot)
{
    var dir = snapshot.Vector2Value;
    // do something
}

void OnStopMove(InputSnapshot snapshot)
{
    // reset velocity
}