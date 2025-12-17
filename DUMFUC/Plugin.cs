using BepInEx;
using BepInEx.Unity.IL2CPP;
using DUMFUC.Input;
using DUMFUC.Tests;
using UnityEngine.XR;

namespace DUMFUC;

[BepInPlugin("com.doomsickle.dumfuc", "DUMFUC", "1.0.0")]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        AddComponent<InputSystem>();
        InputDevices.deviceConnected += new Action<InputDevice>(_ => InputSystemTests.Run());
    }
}