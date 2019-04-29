using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractible : MonoBehaviour
{
    public abstract void Interaction(Player thisPlayer);
}
