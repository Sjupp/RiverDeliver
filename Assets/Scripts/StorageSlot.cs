using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : MonoBehaviour
{
    public Fisherman fisherman = null;
    public FishBox fishBox = null;
    public bool isTaken;
    public bool attachedToFisherman;

    public void AddFishBox(FishBox fb)
    {
        fishBox = fb;
        isTaken = true;
        if (attachedToFisherman)
            fisherman.hasFishBox = true;
    }

    public void RemoveFishBox()
    {
        fishBox = null;
        isTaken = false;
        if (attachedToFisherman)
            fisherman.hasFishBox = false;
    }
}
