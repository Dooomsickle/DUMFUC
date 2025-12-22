using BepInEx.Logging;
using DUMFUC.Input;
using DUMFUC.Input.Enums;
using DUMFUC.Input.Sources;

namespace DUMFUC.Tests;

public static class InputSystemTests
{
    private static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource("InputSystemTests");
    
    public static void Run()
    {
        var action = new InputAction(new Vr2dInputSource(VrInputType.ThumbstickAxis2D, VrHand.Right),
            IInputInteraction.DefaultContinuous());
        
        InputSystem.Initialize();
        InputSystem.Instance.RegisterInputAction(action);

        action.Performed += LogTest;
    }

    public static void LogTest(InputSnapshot snapshot)
    {
        Logger.LogInfo($"{snapshot.Vector2Value}");
    }
}