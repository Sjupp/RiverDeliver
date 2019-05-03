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
    public int extractionAmount = 0;
    private float timer = 2.0f;

    public override void Interaction(Player thisPlayer)
    {
        if (hasFishBox)
        {
            if (!storageSlotRef.fishBox.isEmpty)
            {
                storageSlotRef.fishBox.ChangeFishValue(extractionAmount);
                particleSystem.Emit(2);
                BoatManager.INSTANCE.fishScore += 20;
            }
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            AutomaticExtractionInteraction();
            timer = 2.0f;
        }
    }

    private void AutomaticExtractionInteraction()
    {
        if (hasFishBox)
        {
            if (!storageSlotRef.fishBox.isEmpty)
            {
                storageSlotRef.fishBox.ChangeFishValue(-1);
                particleSystem.Emit(1);
                BoatManager.INSTANCE.fishScore += 10;
            }
        }
    }

}
