using DUMFUC.Input.Enums;
using UnityEngine;

namespace DUMFUC.Input;

/// <summary>
/// A snapshot of an input's state at a specific moment in time.
/// </summary>
public struct InputSnapshot
{
    public EInputType Type;
    public bool BoolValue;
    public float FloatValue;
    public Vector2 Vector2Value;
}