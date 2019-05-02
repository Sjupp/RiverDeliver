using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpState : MonkeyState
{
    private Monkey actor;

    public PickedUpState (Monkey _actor)
    {
        actor = _actor;
    }

    public override void EnterState()
    {
        Debug.Log("Enter PickedUp");
        var hej = actor.GetComponent<Rigidbody>();
        hej.constraints = RigidbodyConstraints.FreezeAll;
        hej.useGravity = false;
        //Drop fishbox if carrying
    }

    public override void ExitState()
    {
        Debug.Log("Exit PickedUp");
        actor.GetComponent<Rigidbody>().useGravity = true;
        var hej = actor.GetComponent<Rigidbody>();
        hej.constraints = RigidbodyConstraints.FreezeRotation;
        hej.useGravity = true;
    }

    public override MonkeyState ReceiveMessage(Messages message)
    {
        return null;
    }

    public override MonkeyState UpdateState(float time)
    {
        //Debug.Log("Update PickedUp");
        return null;
    }
}
