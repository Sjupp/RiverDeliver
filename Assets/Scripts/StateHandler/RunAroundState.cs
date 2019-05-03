using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAroundState : MonkeyState
{
    private Monkey actor;
    private float viewAngle;
    private float lerpStrength;
    private float waypointTimer;
    private float waypointTimerCooldown;
    private Vector3 waypointPos;
    private Vector3 actorPos;
    private Rigidbody rb;

    public RunAroundState(Monkey _actor)
    {
        actor = _actor;
        lerpStrength = .7f;
        waypointTimer = 1.0f;
        waypointTimerCooldown = 1.0f;
        waypointPos = Waypoints.INSTANCE.GetRandomWaypoint();
        actorPos = actor.transform.position;
        rb = actor.GetComponent<Rigidbody>();
    }

    public override void EnterState()
    {
        Debug.Log("Enter RunAroundState");
    }

    public override void ExitState()
    {
        Debug.Log("Exit RunAroundState");
    }

    public override MonkeyState ReceiveMessage(Messages message)
    {
        if (message == Messages.PickUp)
        {
            return new PickedUpState(actor);
        }
        return null;
    }

    public override MonkeyState UpdateState(float time)
    {
        waypointTimer -= time;
        if (waypointTimer <= 0)
        {
            waypointPos = Waypoints.INSTANCE.GetRandomWaypoint();
            waypointTimer = waypointTimerCooldown;

            if (Waypoints.INSTANCE.GetAvailableFishBox() != Vector3.zero && !actor.hasBox)
            {
                Debug.Log("Monkey detected available box");
                return new ChaseBoxState(actor);
            }
        }

        actorPos = actor.transform.position;

        rb.velocity = Vector3.Lerp(rb.velocity, (waypointPos - actorPos), lerpStrength);
        actor.transform.LookAt(waypointPos);

        //viewAngle = Mathf.LerpAngle(viewAngle, Vector3.Angle(actorPos, waypointPos), 100);
        //actor.transform.rotation = Quaternion.AngleAxis(viewAngle, Vector3.up);

        return null;
    }
}
