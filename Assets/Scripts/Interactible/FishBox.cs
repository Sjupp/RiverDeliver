using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBox : BaseInteractible
{
    public GameObject[] fishMaterials;
    public BaseInteractionComponent interactionComponent;
    public GameObject fill;
    public FishBoxSpawner spawner;
    public ParticleSystem glitter;


    [Range(0, 10)]
    public int fishQuantity;
    public int fishQuantityLimit;
    public bool isFull;
    public bool isInSpawn = true;

    private readonly float min = -0.01f, max = 0.61f;

    //Debug
    //public void Start()
    //{
    //    fishQuantity = Random.Range(0, 11);
    //    UpdateBoxStatus();
    //}
    private void Start()
    {
        fishQuantity = 0;
        fishQuantityLimit = 10;
    }

    public override void Interaction(Actor thisActor)
    {
        if (isInSpawn)
        {
            spawner.FishBoxHasBeenPickedUp();
            isInSpawn = false;
            spawner = null;
        }

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
            if (Random.Range(0.0f, 1.0f) > 0.10f)
                fishQuantity++;

            UpdateBoxStatus();
            if (fishQuantity == fishQuantityLimit)
            {
                isFull = true;
                glitter.Play();
            }
        }
    }

    public void AddBigFish()
    {
        fishQuantity = fishQuantityLimit;
        UpdateBoxStatus();
        {
            isFull = true;
            glitter.Play();
        }
    }

    private void UpdateBoxStatus()
    {
        float fillPercentage = (float)fishQuantity / (float)fishQuantityLimit;

        fill.transform.localPosition = new Vector3(0, min + (max * fillPercentage), 0);
    }

}
