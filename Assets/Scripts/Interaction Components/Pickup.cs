using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    FishBox, Monkey
}

// Lord forgive me for I have sinned
public class Pickup : BaseInteractionComponent
{
    public bool canBeStored = true;
    public bool isBeingCarried = false;
    public bool isStored = false;
    public StorageSlot storedLocation = null;
    public PickupType currentType;

    public override void ReceiveInteraction(Player thisActor)
    {
        if (!isBeingCarried)
        {
            if (currentType == PickupType.FishBox)
            {
                if (isStored)
                {
                    storedLocation.RemoveFishBox();
                    storedLocation = null;
                    isStored = false;
                }
                transform.SetParent(thisActor.holdPoint, true);
                transform.localPosition = Vector3.zero;
                isBeingCarried = true;
                thisActor.carryingSomething = true;
            }
            else if (currentType == PickupType.Monkey)
            {
                transform.SetParent(thisActor.holdPoint, true);
                transform.localPosition = Vector3.zero;
                isBeingCarried = true;
                thisActor.carryingSomething = true;
                var monkey = GetComponent<Monkey>();
                monkey.stateMachine.currentState.ExitState();
                monkey.stateMachine.currentState = new PickedUpState(monkey);
                monkey.stateMachine.currentState.EnterState();
            }
            
        }
        else if (isBeingCarried && thisActor.carryingSomething)
        {
            if (currentType == PickupType.FishBox)
            {
                Debug.Log("Dropping FishBox");
                DropFishBox(thisActor);
            }
            if (currentType == PickupType.Monkey)
            {
                Debug.Log("Dropping Monkey");
                DropMonkey(thisActor);
            }
        }
    }

    private void DropFishBox(Player thisActor)
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
            var storageSlot = foundCollider.GetComponent<StorageSlot>();
            storageSlot.AddFishBox(GetComponent<FishBox>());
            storedLocation = storageSlot;

            thisActor.carryingSomething = false;
            isBeingCarried = false;
            isStored = true;
        }
        else
        {
            LayerMask layerMask0 = LayerMask.GetMask("Deck");
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask0);

            var temp = transform.rotation;
            transform.SetParent(thisActor.transform.parent, true);
            transform.SetPositionAndRotation(hit.point, temp);

            thisActor.carryingSomething = false;
            isBeingCarried = false;
        }
    }

    private void DropMonkey(Player thisActor)
    {
        var monkey = GetComponent<Monkey>();

        //LayerMask layerMask0 = LayerMask.GetMask(new string[] { "Water", "Deck"});
        //if (Physics.Raycast(transform.position + Vector3.forward, Vector3.down, out RaycastHit hitWater, Mathf.Infinity, layerMask0))
        //{
        //    if (hitWater.collider.gameObject.layer == LayerMask.GetMask("Water"))
        //    Destroy(monkey.gameObject);
        //    thisActor.carryingSomething = false;
        //    return;
        //}

        //LayerMask layerMask1 = LayerMask.GetMask("Deck");
        LayerMask layerMask1 = LayerMask.GetMask(new string[] { "Water", "Deck" });
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitDeck, Mathf.Infinity, layerMask1);

        var temp = transform.rotation;
        transform.SetParent(thisActor.transform.parent, true);
        transform.SetPositionAndRotation(hitDeck.point, temp);

        monkey.stateMachine.currentState.ExitState();
        monkey.stateMachine.currentState = new IdleState(monkey);
        monkey.stateMachine.currentState.EnterState();

        thisActor.carryingSomething = false;
        isBeingCarried = false;

        Debug.Log(hitDeck.collider.gameObject.layer);

        if (hitDeck.collider.gameObject.layer == 4)
            BoatManager.INSTANCE.monkeys.Remove(monkey.gameObject);
            Destroy(monkey.gameObject);
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
