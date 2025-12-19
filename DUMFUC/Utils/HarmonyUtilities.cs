using System.Reflection;

namespace DUMFUC.Utils;

public static class HarmonyUtilities
{
    public static bool HasValidPrefixReturnType(this MethodInfo method) => method.ReturnType == typeof(void) || method.ReturnType == typeof(bool);
    public static bool HasValidPostfixReturnType(this MethodInfo method) => method.ReturnType == typeof(void);
    
    public static bool IsParameterValidHarmony(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        
        // field names
        if (name.StartsWith("___")) return true;
        
        // args via index
        if (name.StartsWith("__") && char.IsNumber(name[2])) return true;
        
        return name switch
        {
            "__instance"
                or "__result"
                or "__resultRef"
                or "__state"
                or "__args"
                or "__originalMethod"
                or "_runOriginal" => true,
            _ => false
        };
    }
}