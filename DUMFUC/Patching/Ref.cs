using System.Reflection;

namespace DUMFUC.Patching;

/// <summary>
/// Allows reference to an empty value of a generic type, usually to get a reference to an instance method.
/// </summary>
/// <typeparam name="T">The type to reference.</typeparam>
public static class Ref<T>
{
    /// <summary>
    /// An empty reference to the type.
    /// </summary>
    public static T? Empty { get; } = default;

    public static Type Type => typeof(T);
    
    /// <summary>
    /// Gets all methods on the type with the given name. Made for dealing with overloads.
    /// </summary>
    /// <param name="name">The name of the function to search for.</param>
    /// <returns>A list of all overloads for that function, or empty if none.</returns>
    public static List<MethodInfo> MethodsNamed(string name) => Type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Where(m => m.Name == name).ToList();
    
    /// <summary>
    /// Finds the first overload of a method with the given name and parameters.
    /// </summary>
    /// <param name="name">The name of the method to find.</param>
    /// <param name="paramTypes">The types of its parameters, in order.</param>
    /// <returns>The found <c>MethodInfo</c>, or <c>null</c> if not found.</returns>
    public static MethodInfo? MethodNamedWithParams(string name, params Type[] paramTypes) => MethodsNamed(name).FirstOrDefault(m => m.GetParameters().Select(p => p.ParameterType).SequenceEqual(paramTypes));
}