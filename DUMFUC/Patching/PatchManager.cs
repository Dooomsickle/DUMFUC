using System.Reflection;
using DUMFUC.Utils;
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
            
            // harmony will fail to patch if the patch method has extra parameters
            var badParams = patch.GetInvalidParameters();
            if (badParams.Any())
            {
                _errors.Add((patch, new ArgumentException($"Invalid parameters: {string.Join(", ", badParams)}")));
                errors++;
                continue;
            }

            // prefix can only return void or bool, postfix can only return void
            if ((!patch.Pfx?.method.HasValidPrefixReturnType() ?? false) ||
                (!patch.Pst?.method.HasValidPostfixReturnType() ?? false))
            {
                _errors.Add((patch, new ArgumentException("Prefix or postfix return type is invalid.")));
                errors++;
                continue;
            }
            
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
            : AppDomain.CurrentDomain.GetAssemblies().Where(a => a.Location.Contains("BepInEx") && a.Location.Contains("plugins"));

        var patches = new List<PatchDefinition>();
        
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(t => typeof(PatchModule).IsAssignableFrom(t));

            var patchDefs = types
                .Select(Activator.CreateInstance)
                .OfType<PatchModule>()
                .SelectMany(p => p.Define())
                .ToList();
            
            patches.AddRange(patchDefs);
        }
        
        return patches;
    }
}