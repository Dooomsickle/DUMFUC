using DUMFUC.Input.Enums;

namespace DUMFUC.Input.Config;

[Serializable]
public class InteractionDefinition
{
    public EInteractionType interactionType;

    // Hold interaction data
    public float deadzone;
    public float holdTime;
    public bool continuous;
}