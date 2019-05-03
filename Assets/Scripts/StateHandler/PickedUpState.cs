using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpState : MonkeyState
{
    private Monkey actor;
    private Rigidbody rb;

    public PickedUpState (Monkey _actor)
    {
        actor = _actor;
        rb = actor.GetComponent<Rigidbody>();
    }

    public override void EnterState()
    {
        Debug.Log("Enter PickedUp");
        if (actor.hasBox)
            CustomMonkeyDrop();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;
    }

    public override void ExitState()
    {
        Debug.Log("Exit PickedUp");
        actor.GetComponent<Rigidbody>().useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = true;
    }

    public override MonkeyState ReceiveMessage(Messages message)
    {
        return null;
    }

    public override MonkeyState UpdateState(float time)
    {
        return null;
    }

    private void CustomMonkeyDrop() // rest in peace component system 
    {
        Debug.Log("CustomMonkeyDrop");
        LayerMask layerMask0 = LayerMask.GetMask("Deck");
        Physics.Raycast(actor.transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask0);

        var pickups = actor.GetComponentsInChildren<Pickup>();
        Pickup pickup = null;
        foreach (var item in pickups)
        {
            if (item.gameObject != actor.gameObject)
                pickup = item;
        }

        if (!pickup)
            return;

        var rot = actor.transform.rotation;
        pickup.transform.SetParent(GameObject.Find("Actor Plane").transform, true);
        pickup.transform.SetPositionAndRotation(hit.point, rot);
        pickup.isBeingCarried = false;
        actor.hasBox = false;
    }
}
