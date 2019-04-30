using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : MonoBehaviour
{
    [Header("Editor")]
    public Fisherman fisherman = null;
    public bool attachedToFisherman;
    public Extractor extractor = null;
    public bool attachedToExtractor;

    [Header("Runtime")]
    public FishBox fishBox = null;
    public bool isTaken;

    public void AddFishBox(FishBox fb)
    {
        fishBox = fb;
        isTaken = true;
        if (attachedToFisherman)
            fisherman.hasFishBox = true;
        else if (attachedToExtractor)
            extractor.hasFishBox = true;
            
    }

    public void RemoveFishBox()
    {
        fishBox = null;
        isTaken = false;
        if (attachedToFisherman)
            fisherman.hasFishBox = false;
        else if (attachedToExtractor)
            extractor.hasFishBox = false;
    }
}
