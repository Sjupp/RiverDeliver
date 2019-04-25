using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBoxSpawner : MonoBehaviour
{
    public GameObject fishBoxPrefab;
    private FishBox currentFishBox;

    private bool canFish = true;

    private float fishCatchTimer;
    public float fishCatchTime = 10f;

    private void Update() //TODO: Run a custom update via a GameManager later
    {
        if (canFish)
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
        if (!currentFishBox)
            SpawnNewFishBox();

        if (!currentFishBox.isFull)
        {
            currentFishBox.AddFish();
        }

        if (currentFishBox.isFull)
            canFish = false;
    }

    private void SpawnNewFishBox()
    {
        var fb = Instantiate<GameObject>(fishBoxPrefab, transform, false);
        currentFishBox = fb.GetComponent<FishBox>();
        currentFishBox.spawner = this;
    }

    public void SucceedWithHelpingEvent()
    {
        currentFishBox.AddBigFish();
        ExecuteTacticalFishingManeuver();
    }

    public void FishBoxHasBeenPickedUp()
    {
        currentFishBox = null;
        canFish = true;
        ExecuteTacticalFishingManeuver(); //TODO: Maybe not do this?
    }
}
