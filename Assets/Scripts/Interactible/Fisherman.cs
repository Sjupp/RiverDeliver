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
            storageSlotRef.fishBox.AddFish();
            //canFish = false;
    }

    public void SucceedWithHelpingEvent()
    {
        storageSlotRef.fishBox.AddBigFish();
        ExecuteTacticalFishingManeuver();
    }

    public override void Interaction(Player thisActor)
    {
        throw new System.NotImplementedException();
    }
}
