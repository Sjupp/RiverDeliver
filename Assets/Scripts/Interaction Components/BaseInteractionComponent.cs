using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractionComponent : MonoBehaviour
{
    public abstract void ReceiveInteraction(Player thisActor);
}
