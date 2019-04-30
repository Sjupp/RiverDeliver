using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisherman : BaseInteractible
{
    public StorageSlot storageSlotRef;

    public bool canFish = true;
    public bool hasFishBox = false;

    private float fishCatchTimer;
    public float fishCatchTime = 10f;

    private void Update() //TODO: Run a custom update via a GameManager later
    {
        if (canFish && hasFishBox)
        {
            if (fishCatchTimer <= 0)
            {
                ExecuteTacticalFishingManeuver();
                fishCatchTimer = fishCatchTime;
            }
            fishCatchTimer -= Time.deltaTime;
        }
    }

    private void ExecuteTacticalFishingManeuver()
    {

        if (!storageSlotRef.fishBox.isFull)
            storageSlotRef.fishBox.ChangeFishValue(1);
            //canFish = false;
    }

    public void SucceedWithHelpingEvent()
    {
        storageSlotRef.fishBox.ChangeFishValue(10);
        ExecuteTacticalFishingManeuver();
    }

    public override void Interaction(Player thisActor)
    {
        Debug.Log("Interacted with: " + name);
    }
}
