using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using DUMFUC.Patching;
using UnityEngine.Assertions;
using UnityEngine.Playables;

namespace DUMFUC;

[BepInPlugin("com.doomsickle.dumfuc", "DUMFUC", "1.0.0")]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        PatchManager.ApplyPatches(typeof(Plugin).Assembly);
        
        var logger = Logger.CreateLogSource("Plugin");
        logger.LogInfo("Testing!");
    }
}

public class TestPatch : PatchModule
{
    private static void MyPatch(ref object __0) => __0 = "Patched!";
    
    public override IEnumerable<PatchDefinition> Define()
    {
        yield return Target(Ref<ManualLogSource>.MethodsNamed("LogInfo"))
            .Prefix(MyPatch);
    }
}