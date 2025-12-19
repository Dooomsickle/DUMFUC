namespace DUMFUC.Attributes;

/// <summary>
/// Signals to obfuscators to not rename whatever this is applied to, typically used when strong naming is required such as in a Harmony patch.
/// For ConfuserEx/2, it's a shorthand for <c>Obfuscation(Exclude=false, Feature="-rename")</c>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property)]
public class NoRenameAttribute : Attribute { }