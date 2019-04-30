using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Monkey : BaseInteractible
{
    private MonkeyState currentMonkeyState;

    public void Start()
    {
        currentMonkeyState = MonkeyState.JumpIn;
    }

    private void Update()
    {
        switch (currentMonkeyState)
        {
            case MonkeyState.JumpIn:
                {
                    //Await "you have spawned in"
                    break;
                }
            case MonkeyState.GoForBox:
                {
                    //Find nearest box and move towards it
                    break;
                }
            case MonkeyState.RunAround:
                {
                    //Run around aimlessly
                    break;
                }
            case MonkeyState.PickedUp:
                {
                    //Do Nothing
                break;
                }
            default:
                break;
        }
    }


    public BaseInteractionComponent interactionComponent;

    public override void Interaction(Player thisActor)
    {
        interactionComponent.ReceiveInteraction(thisActor);
    }
}
