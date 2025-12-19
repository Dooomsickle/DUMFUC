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
```

## Defining a new input source
```csharp
public sealed class MouseDirectionInputSource : AInputSource
{
    public override EInputType InputType => EInputType.Axis2D;
    public override EInputDevice Device => EInputDevice.Mouse; // XrController, Keyboard, Mouse, or Peripheral
    
    public MouseDirectionInputSource() { }
    
    public override bool GetBool() => false;
    public override float GetFloat() => 0f;
    public override Vector2 GetVector2() => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
}

// can later be used in an action
var lookAction = new InputAction(
    new MouseDirectionInputSource(),
    IInputInteraction.DefaultContinuous() // fires every frame a value is given
    );
```