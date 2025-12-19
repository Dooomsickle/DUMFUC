using System.Reflection;
using HarmonyLib;

namespace DUMFUC.Patching;

public static class PatchManager
{
    private static readonly Harmony _harmonyInstance = new("doomsickle.dumfuc");
    private static List<PatchDefinition> _donePatches = new();
    private static List<(PatchDefinition patch, Exception what)> _errors = new();

    /// <summary>
    /// Discovers and applies Harmony patches to an assembly.
    /// </summary>
    /// <param name="asm">The target assembly. If <c>null</c>, applies patches in all assemblies located in the BepInEx/plugins folder.</param>
    /// <returns>The number of errors that occurred, if any.</returns>
    public static int ApplyPatches(Assembly? asm = null)
    {
        int errors = 0;
        
        var patches = DiscoverPatches(asm);
        
        // horrible ugly nesting ugghhhhggh
        foreach (var patch in patches)
        {
            if (_donePatches.Contains(patch)) continue;
            
            try
            {
                patch.Apply(_harmonyInstance);
                _donePatches.Add(patch);
            }
            catch (Exception ex)
            {
                _errors.Add((patch, ex));
                errors++;
            }
        }
        
        return errors;
    }
    
    /// <summary>
    /// Gets all errors that occurred during patching.
    /// </summary>
    /// <returns>An enumerable of the errors and the patches that caused them.</returns>
    public static IEnumerable<(PatchDefinition patch, Exception what)> GetErrors() => _errors;
    
    /// <summary>
    /// Gets a specific error that occurred during patching.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>A tuple of the error and the patch that caused it.</returns>
    public static (PatchDefinition patch, Exception what)? GetError(int index) => _errors.ElementAtOrDefault(index);

    private static List<PatchDefinition> DiscoverPatches(Assembly? asm = null)
    {
        var assemblies = asm != null
            ? new[] { asm }
            : AppDomain.CurrentDomain.GetAssemblies().Where(a => a.Location.Contains("plugins"));

        var patches = new List<PatchDefinition>();
        
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(t => typeof(APatchModule).IsAssignableFrom(t));

            var patchDefs = assembly.GetTypes()
                .Where(t => typeof(APatchModule).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .OfType<APatchModule>()
                .SelectMany(p => p.Define())
                .ToList();
            
            patches.AddRange(patchDefs);
        }
        
        return patches;
    }
}