using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBox : BaseInteractible
{
    public GameObject[] fishMaterials;
    public BaseInteractionComponent interactionComponent;
    public GameObject fill;
    public ParticleSystem glitter;

    [Range(0, 10)]
    public int fishQuantity;
    public int fishQuantityLimit;
    public bool isFull;
    public bool isEmpty;

    private readonly float min = 0.09f, max = 0.51f;

    private void Start()
    {
        isFull = false;
        isEmpty = true;
        fishQuantity = 0;
        fishQuantityLimit = 10;
    }

    public override void Interaction(Player thisActor)
    {
        interactionComponent.ReceiveInteraction(thisActor);
    }

    private void SetMaterial()
    {
        foreach (GameObject go in fishMaterials)
        {
            go.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

    public void ChangeFishValue(int amount)
    {
        var newFishQuantity = fishQuantity + amount;

        if (newFishQuantity >= fishQuantityLimit)
        {
            fishQuantity = fishQuantityLimit;
            isFull = true;
        }
        else if (newFishQuantity <= 0)
        {
            fishQuantity = 0;
            isEmpty = true;
        }
        else
        {
            fishQuantity = newFishQuantity;
            isEmpty = false;
            isFull = false;
        }

        UpdateBoxVisuals();
    }

    private void UpdateBoxVisuals()
    {
        if (isFull)
            glitter.Play();
        else
            glitter.Stop();

        float fillPercentage = (float)fishQuantity / (float)fishQuantityLimit;
        fill.transform.localPosition = new Vector3(0, min + (max * fillPercentage), 0);
    }

}
