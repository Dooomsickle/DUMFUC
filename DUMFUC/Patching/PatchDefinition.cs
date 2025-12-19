using System.Reflection;
using DUMFUC.Utils;
using HarmonyLib;

namespace DUMFUC.Patching;

/// <summary>
/// Represents a patch to be applied to a method.
/// </summary>
public sealed class PatchDefinition
{
    /// <summary>
    /// The methods to patch.
    /// </summary>
    public List<MethodInfo> Targets { get; }
    
    /// <summary>
    /// The method that will run before the target when patched.
    /// </summary>
    public HarmonyMethod? Pfx { get; set; }
    
    /// <summary>
    /// The method that will run after the target when patched.
    /// </summary>
    public HarmonyMethod? Pst { get; set; }
    
    public PatchDefinition(List<MethodInfo> targets) => Targets = targets;

    /// <summary>
    /// Assigns a prefix (runs before) method to the target.
    /// </summary>
    /// <param name="method">The method to run before.</param>
    /// <returns>The patch definition, for fluence.</returns>
    /// <exception cref="ArgumentException">if the prefix method does not return void or bool.</exception>
    public PatchDefinition Prefix(Delegate method)
    {
        if (method.Method.ReturnType != typeof(void) && method.Method.ReturnType != typeof(bool))
            throw new ArgumentException("Prefix method must return void or bool.");
        
        Pfx = new HarmonyMethod(method.Method);
        return this;
    }

    /// <summary>
    /// Assigns a postfix (runs after) method to the target.
    /// </summary>
    /// <param name="method">The method to run after.</param>
    /// <returns>The patch definition, for fluence.</returns>
    /// <exception cref="ArgumentException">if the postfix method does not return void.</exception>
    public PatchDefinition Postfix(Delegate method)
    {
        if (method.Method.ReturnType != typeof(void))
            throw new ArgumentException("Postfix method must return void.");
        
        Pst = new HarmonyMethod(method.Method);
        return this;
    }

    /// <summary>
    /// Compiles and applies this patch with the given Harmony instance.
    /// </summary>
    /// <param name="to">The Harmony instance.</param>
    public void Apply(Harmony to)
    {
        if (Pfx == null && Pst == null)
            throw new ArgumentException("Empty patch.");
        
        if (Targets.Count == 0) 
            throw new ArgumentException("No targets.");
        
        foreach (var target in Targets)
            to.Patch(target, Pfx, Pst);
    }

    /// <summary>
    /// Finds any problematic parameters in the prefix or postfix method that would cause an error when applied.
    /// </summary>
    /// <returns>A list containing the erroneous parameter names, or an empty one if none.</returns>
    public List<string> GetInvalidParameters()
    {
        if (Targets.Count == 0) return new List<string>();
        if (Pfx == null && Pst == null) return new List<string>();
        
        var targetParamNames = Targets[0].GetParameters()
            .Select(p => p.Name ?? "")
            .ToHashSet();

        return new[] { Pfx?.method, Pst?.method }
            .Where(m => m != null)
            .SelectMany(m => m!.GetParameters())
            .Select(p => p.Name ?? "")
            // if a parameter name isn't in the target, check if it's supposed to be injected
            .Where(name => !targetParamNames.Contains(name) && !HarmonyUtilities.IsParameterValidHarmony(name))
            .Distinct()
            .ToList();
    }
}