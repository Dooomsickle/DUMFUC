# Patching Examples

## Defining a patch for a static method
```csharp
public class TargetExample
{
    public static void ExampleStatic(string hello)
    {
        Console.WriteLine(hello);
    }
    
    public void ExampleInstance(string hello)
    {
        Console.WriteLine(hello);
    }
    
    public void ExampleOverload(string hello)
    {
        Console.WriteLine(hello);
    }
    
    public void ExampleOverload(int hello)
    {
        Console.WriteLine(hello);
    }
}

public class PatchExample : PatchModule
{
    // names of parameters must match to access them, or you can use the index
    private static void MyPatch(ref string hello)
    {
        hello = "Patched!";
    }
    
    public override IEnumerable<PatchDefinition> Define()
    {
        // patches for static methods can be made directly
        yield return Target(TargetExample.ExampleStatic)
            .Prefix(MyPatch); 
    }
}
```

## Defining a patch for an instance method
```csharp
public class PatchExample : PatchModule
{
    private void MyPatch(ref string hello)
    {
        hello = "Patched!";
    }
    
    public override IEnumerable<PatchDefinition> Define()
    {
        // since instance methods can't be referred to directly, we need an empty instance
        yield return Target(Ref<TargetExample>.Empty.ExampleInstance)
            .Prefix(MyPatch);
    }
```

## Defining a patch for all overloads of a method
```csharp
public class PatchExample : PatchModule
{
    // parameter types still need to be compatible with all overloads
    private bool MyPatch(object hello)
    {
        Console.WriteLine("Patched overload!");
        return false; // skip original
    }
    
    public override IEnumerable<PatchDefinition> Define()
    {
        // even though this was designed to avoid string references, it's unavoidable here
        // still shorter than defining HarmonyTargetMethods
        yield return Target(Ref<TargetExample>.MethodsNamed(nameof(TargetExample.ExampleOverload))
            .Prefix(MyPatch);
    }
}
```

## Defining a patch for a specific overload
```csharp
public class PatchExample : PatchModule
{
    private static void MyPatch(ref string hello)
    {
        hello = "Patched!";
    }
    
    public override IEnumerable<PatchDefinition> Define()
    {
        yield return Target(Ref<TargetExample>
            .MethodNamedWithParams(nameof(TargetExample.ExampleOverload), typeof(string)))
            .Prefix(MyPatch);
    }
}