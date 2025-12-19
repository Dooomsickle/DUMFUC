using DUMFUC.Tests;

namespace DUMFUC.Patching;

/// <summary>
/// Allows grouping of Harmony patches into a single file/class.
/// </summary>
public abstract class APatchModule
{
    protected static PatchDefinition Target(Delegate method) => new(method);

    public abstract IEnumerable<PatchDefinition> Define();
}

public class Test : APatchModule
{
    public override IEnumerable<PatchDefinition> Define()
    {
        yield return Target(InputSystemTests.Run)
            .Prefix(Define);
    }
}