using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBoxState : MonkeyState
{
    private Monkey actor;
    private Vector3 chaseTarget;
    //private float viewAngle; Tried to lerp monkey direction, but turned out to be troublesome.
    private float lerpStrength;
    private Vector3 actorPos;
    private Rigidbody rb;

    public ChaseBoxState(Monkey _actor)
    {
        actor = _actor;
        chaseTarget = Vector3.zero;

        lerpStrength = .7f;
        actorPos = actor.transform.position;
        rb = actor.GetComponent<Rigidbody>();
    }

    public override void EnterState()
    {
        Debug.Log("Monkey chasing a box");
        chaseTarget = Waypoints.INSTANCE.GetAvailableFishBox();
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
        actorPos = actor.transform.position;
        rb.velocity = Vector3.Lerp(rb.velocity, (chaseTarget - actorPos), lerpStrength);
        actor.transform.LookAt(chaseTarget);
        if (Vector3.Distance(actor.transform.position, chaseTarget) < 2.5f)
        {
            Debug.Log("Monkey tried picking up box and returning to run around state");
            CustomMonkeyPickup();
            return new RunAroundState(actor);
        }
        return null;
    }


    private void CustomMonkeyPickup() // ohgod.jpeg 
    {
        LayerMask layerMask = LayerMask.GetMask("Interactible");
        Collider[] foundInteractibles = Physics.OverlapBox(actor.transform.position, Vector3.one * 3, Quaternion.identity, layerMask);
        foreach (var item in foundInteractibles)
        {
            if (item.CompareTag("FishBox"))
            {
                if (item.GetComponent<Pickup>().isStored)
                {
                    var temp = item.GetComponent<Pickup>();
                    temp.storedLocation.RemoveFishBox();
                    temp.storedLocation = null;
                    temp.isStored = false;
                    temp.transform.SetParent(actor.transform, true);
                    temp.transform.localPosition = Vector3.up * 2.5f;
                    temp.isBeingCarried = true;
                    actor.hasBox = true;
                }
            }
        }
    }
}
