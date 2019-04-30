using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : BaseInteractible
{
    public StorageSlot storageSlotRef;
    public ParticleSystem particleSystem;
    public bool hasFishBox;
    [Header("Amount of fish extracted per button press")]
    [Range(-5, -1)]
    public int extractionAmount = 1;

    public override void Interaction(Player thisPlayer)
    {
        if (hasFishBox)
        {
            if (!storageSlotRef.fishBox.isEmpty)
            {
                storageSlotRef.fishBox.ChangeFishValue(extractionAmount);
                particleSystem.Emit(1);
            }
        }
        Debug.Log("Interacted with: " + name);
    }
}
