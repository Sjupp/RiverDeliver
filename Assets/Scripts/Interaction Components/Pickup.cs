using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : BaseInteractionComponent
{
    public bool canBeStored = true;
    private bool isBeingCarried = false;
    private bool isStored = false;
    private Player player = null;

    public override void ReceiveInteraction(Actor thisActor)
    {
        if (!isBeingCarried && !isStored)
        {
            player = thisActor as Player; //TODO: Probably remove Actor base class b/c superfluous.
            transform.SetParent(player.holdPoint, true);
            transform.localPosition = Vector3.zero;
            isBeingCarried = true;
            player.carryingSomething = true;
        }
        else if (isBeingCarried && player.carryingSomething)
        {
            Drop(thisActor);
        }
    }

    private void Drop(Actor thisActor)
    {
        bool storageAvailable = false;
        Collider foundCollider = null;

        if (canBeStored)
        {
            LayerMask layerMask = LayerMask.GetMask("Storage");
            Collider[] foundColliders = Physics.OverlapBox(transform.position, new Vector3(1.5f, 2f, 1.5f), Quaternion.identity, layerMask);

            foundCollider = GetNearestCollider(foundColliders); //will return null if colliders were found but all were full / taken
            if (foundCollider)
                storageAvailable = true;
        }

        if (storageAvailable)
        {
            transform.SetParent(foundCollider.transform, false);
            foundCollider.GetComponent<StorageSlot>().isTaken = true;

            player.carryingSomething = false;
            isBeingCarried = false;
            isStored = true;
        }
        else
        {
            LayerMask layerMask0 = LayerMask.GetMask("Deck");
            // Still haven't figured out how to use these outside of an if-statement
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask0)) ;

            var temp = transform.rotation;
            transform.SetParent(thisActor.transform.parent, true);
            transform.SetPositionAndRotation(hit.point, temp);

            player.carryingSomething = false;
            isBeingCarried = false;
        }
    }

    //Stulen från https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/ post #4
    private Collider GetNearestCollider(Collider[] colliderArray)
    {
        Collider bestCollider = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider collider in colliderArray)
        {
            if (!collider.GetComponent<StorageSlot>().isTaken)
            {
                Vector3 directionToTarget = collider.transform.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestCollider = collider;
                }
            }
        }
        return bestCollider;
    }
}
