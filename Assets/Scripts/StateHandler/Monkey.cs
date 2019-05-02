using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : BaseInteractible //Monkey but manager, good promotion
{
    private MonkeyState currentState;
    public BaseInteractionComponent interactionComponent;
    public StateMachine stateMachine;

    public bool hasBox = false;

    void Start()
    {
        stateMachine = new StateMachine(this);
        stateMachine.currentState = new JumpInState(this);
    }

    public void AttemptPickup()
    {
        stateMachine.ReceiveMessage(MonkeyState.Messages.PickUp);
    }

    public void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }

    public override void Interaction(Player thisActor)
    {
        interactionComponent.ReceiveInteraction(thisActor);
    }
}
