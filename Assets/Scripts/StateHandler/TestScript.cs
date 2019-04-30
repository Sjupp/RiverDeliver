using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonkeyState
{
    JumpIn, GoForBox, RunAround, PickedUp, Idle, Attack
}

public class TestScript : MonoBehaviour //Monkey but manager, good promotion
{
    private IdleState idleState;
    private AttackState attackState;
    private StateMachine stateMachine;
    private MonkeyState currentState;

    void Start()
    {
        stateMachine = new StateMachine();
        idleState = new IdleState();
        attackState = new AttackState();
        ChangeState(MonkeyState.Idle, idleState);
    }

    public void SendInMonkey()
    {
        ChangeState(MonkeyState.JumpIn, idleState);
    }




    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        ChangeState(MonkeyState.Idle, idleState);
    //    }
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        ChangeState(MonkeyState.Attack, attackState);
    //        Invoke("StopAttack", 1.0f);
    //    }
    //}

    private void StopAttack()
    {
        stateMachine.ChangeState(idleState);
        currentState = MonkeyState.Idle;
    }

    private void ChangeState(MonkeyState  newCurrentState, State newState)
    {
        if (currentState != newCurrentState)
        {
            currentState = newCurrentState;
            stateMachine.ChangeState(newState);
        }
    }

}
