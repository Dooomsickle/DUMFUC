using System.Reflection;

namespace DUMFUC.Patching;

/// <summary>
/// Allows grouping of Harmony patches into a single file/class.
/// </summary>
public class PatchModule
{
    /// <summary>
    /// Defines a patch target.
    /// </summary>
    /// <param name="method">The method to target.</param>
    /// <returns>A new patch definition describing the target.</returns>
    protected static PatchDefinition Target(Delegate method) => new(new List<MethodInfo>() {method.Method});
    
    /// <summary>
    /// Defines patch targets.
    /// </summary>
    /// <param name="methods">The methods to target.</param>
    /// <returns>A new patch definition describing the targets.</returns>
    protected static PatchDefinition Target(params Delegate[] methods) => new(methods.Select(m => m.Method).ToList());
    
    /// <summary>
    /// Defines patch targets.
    /// </summary>
    /// <param name="methods">The methods to target.</param>
    /// <returns>A new patch definition describing the targets.</returns>
    protected static PatchDefinition Target(List<Delegate> methods) => new(methods.Select(m => m.Method).ToList());
    
    /// <summary>
    /// Defines a patch target.
    /// </summary>
    /// <param name="method">The method to target.</param>
    /// <returns>A new patch definition describing the target.</returns>
    protected static PatchDefinition Target(MethodInfo method) => new(new List<MethodInfo>() {method});
    
    /// <summary>
    /// Defines patch targets.
    /// </summary>
    /// <param name="methods">The methods to target.</param>
    /// <returns>A new patch definition describing the targets.</returns>
    protected static PatchDefinition Target(params MethodInfo[] methods) => new(methods.ToList());
    
    /// <summary>
    /// Defines patch targets.
    /// </summary>
    /// <param name="methods">The methods to target.</param>
    /// <returns>A new patch definition describing the targets.</returns>
    protected static PatchDefinition Target(List<MethodInfo> methods) => new(methods);

    /// <summary>
    /// Defines the patches to apply. Should not be called directly.
    /// </summary>
    /// <returns>An enumerable of defined patches.</returns>
    public virtual IEnumerable<PatchDefinition> Define() => Array.Empty<PatchDefinition>();
}