namespace DUMFUC.Input.Enums;

/// <summary>
/// The different states an input action can be in.
/// <list type="bullet">
/// <item>Started - The action has just been initiated.</item>
/// <item>Performed - The action is currently being executed.</item>
/// <item>Canceled - The action has been stopped.</item>
/// </list>
/// </summary>
public enum ActionPhase
{
    Started,
    Performed,
    Canceled
}