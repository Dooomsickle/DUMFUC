using HarmonyLib;

namespace DUMFUC.Patching;

public sealed class PatchDefinition
{
    public Delegate Target { get; }
    
    public HarmonyMethod? Pfx { get; set; }
    public HarmonyMethod? Pst { get; set; }
    
    public PatchDefinition(Delegate target)
    {
        Target = target;
    }
    
    public PatchDefinition Prefix(Delegate method)
    {
        Pfx = new HarmonyMethod(method.Method);
        return this;
    }

    public PatchDefinition Postfix(Delegate method)
    {
        Pst = new HarmonyMethod(method.Method);
        return this;
    }

    public void Apply(Harmony to) => to.Patch(Target.Method, Pfx, Pst);
}