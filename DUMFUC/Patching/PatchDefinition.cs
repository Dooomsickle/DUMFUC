using HarmonyLib;

namespace DUMFUC.Patching;

/// <summary>
/// Represents a patch to be applied to a method.
/// </summary>
public sealed class PatchDefinition
{
    /// <summary>
    /// The method to patch.
    /// </summary>
    public Delegate Target { get; }
    
    /// <summary>
    /// The method that will run before the target when patched.
    /// </summary>
    public HarmonyMethod? Pfx { get; set; }
    
    /// <summary>
    /// The method that will run after the target when patched.
    /// </summary>
    public HarmonyMethod? Pst { get; set; }
    
    public PatchDefinition(Delegate target) => Target = target;

    /// <summary>
    /// Assigns a prefix (runs before) method to the target.
    /// </summary>
    /// <param name="method">The method to run before.</param>
    /// <returns>The patch definition, for fluence.</returns>
    /// <exception cref="ArgumentException">if the prefix method does not return void or bool.</exception>
    public PatchDefinition Prefix(Delegate method)
    {
        if (method.Method.ReturnType != typeof(void) || method.Method.ReturnType != typeof(bool))
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
        
        to.Patch(Target.Method, Pfx, Pst);
    }
}