namespace DUMFUC.Patching;

/// <summary>
/// Allows grouping of Harmony patches into a single file/class.
/// </summary>
public abstract class APatchModule
{
    /// <summary>
    /// Defines a patch target.
    /// </summary>
    /// <param name="method">The method to target.</param>
    /// <returns>A new patch definition describing the target.</returns>
    protected static PatchDefinition Target(Delegate method) => new(method);

    public abstract IEnumerable<PatchDefinition> Define();
}