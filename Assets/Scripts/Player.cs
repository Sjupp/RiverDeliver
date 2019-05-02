using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal bool carryingSomething = false;

    public Transform boxCollider;
    public Transform holdPoint;

    private Vector3 interactionOverlapSize = new Vector3(1.5f, 2f, 1.5f);

    public void Broom()
    {
        if (!carryingSomething) ;
        
        //Raycast
        //If Hit Monkey > Shoo
        //If Hit Puddle > Mop
    }
    
    public void Interact()
    {

        LayerMask layerMask = LayerMask.GetMask("Interactible");
        Collider[] foundInteractibles = Physics.OverlapBox(boxCollider.position, interactionOverlapSize, Quaternion.identity, layerMask);


        //Implement Priorities Here


        if (foundInteractibles.Length >= 1)
            GetNearestCollider(foundInteractibles).GetComponent<BaseInteractible>().Interaction(this);

        /*
         Prioritize(allColliders[]);
             Get all colliders
             Sort into Lists
             Check List priority
             Discard lower priority Lists
             Check List Count
             If == 1 Select it, If > 1 Select Nearest To Center
         Interact(SelectedThing);
            Do The Thing(tm)

         What are the lists?
         Think of them as sorted types of interactions
         1. Carry
         2. Button Mash
         3. ??

         - Button Used (What interaction type was used)
         - Reaction (What happens when corresponding thing gets interacted with)

        Interactible        <-      Carryable       <-          FishBox
        declares interact();        PickUp();                   hej
                                    Drop();
         */
    }

    //Stulen från https://forum.unity.com/threads/clean-est-way-to-find-nearest-object-of-many-c.44315/ post #4
    private Collider GetNearestCollider(Collider[] colliderArray)
    {
        Collider bestCollider = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider collider in colliderArray)
        {
            Vector3 directionToTarget = collider.transform.position - holdPoint.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestCollider = collider;
            }
        }
        return bestCollider;
    }



}

/*
 Context priority lists

    Interact
    1. Drop Fish Box if carrying one (carry)
    2. Help with Big Fish (button mash)
    3. Pick up Fish Box (carry)

    Broom
    1. Swipe at Monkey (just: if hit, activate)
    2. Mop Spill (button Mash)
*/