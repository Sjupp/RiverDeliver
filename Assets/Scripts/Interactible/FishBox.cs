using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBox : BaseInteractible
{
    public GameObject[] fishMaterials;
    public BaseInteractionComponent interactionComponent;
    public GameObject fill;

    [Range(0, 10)]
    public int fishQuantity = 0;
    public int fishQuantityLimit = 10;
    public bool fishBoxIsFull;

    private readonly float min = -0.01f, max = 0.61f;

    public void Start()
    {
        fishQuantity = Random.Range(0, 11);
        UpdateBoxStatus();
    }

    public override void Interaction(Actor thisActor)
    {
        interactionComponent.ReceiveInteraction(thisActor);
    }

    private void SetMaterial()
    {
        Random.Range(1, 2);
        foreach (GameObject go in fishMaterials)
        {
            go.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    

    public void AddFish()
    {
        if (fishQuantity < fishQuantityLimit)
        {
            fishQuantity++;
            UpdateBoxStatus();
            if (fishQuantity == fishQuantityLimit)
                fishBoxIsFull = true;
        }
    }

    public void AddBigFish()
    {
        fishQuantity = fishQuantityLimit;
        UpdateBoxStatus();
        fishBoxIsFull = true;
    }

    private void UpdateBoxStatus()
    {
        float fillPercentage = (float)fishQuantity / (float)fishQuantityLimit;

        fill.transform.localPosition = new Vector3(0, min + (max * fillPercentage), 0);
    }

}
