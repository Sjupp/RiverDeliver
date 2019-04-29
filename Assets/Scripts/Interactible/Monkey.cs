using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : BaseInteractible
{
    public BaseInteractionComponent interactionComponent;

    public override void Interaction(Player thisActor)
    {
        interactionComponent.ReceiveInteraction(thisActor);
    }
}
