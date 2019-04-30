using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void EnterState(Actor actor)
    {
        Debug.Log("Enter Idle");
        //Animator Play(JumpIn);
    }

    public override void ExitState(Actor actor)
    {
        Debug.Log("Exit Idle");
    }

    public override void UpdateState(Actor actor)
    {
        Debug.Log("Update Idle");
    }
}
