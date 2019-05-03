using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInState : MonkeyState
{
    private Monkey actor;
    private float durationTime;

    public JumpInState(Monkey _actor)
    {
        actor = _actor;
        durationTime = 2.0f;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override MonkeyState ReceiveMessage(Messages message)
    {
        return null;
    }

    public override MonkeyState UpdateState(float time)
    {
        durationTime -= time;
        if (durationTime <= 0)
        {
            return new RunAroundState(actor);
        }
        return null;
    }
}
