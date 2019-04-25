using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public bool enableFloatiness = false;
    float incrementalNumber = 0;
    void Update()
    {
        incrementalNumber += Time.deltaTime;
        if (enableFloatiness)
            gameObject.transform.rotation = Quaternion.Euler(Mathf.Sin(incrementalNumber + 1), -90, Mathf.Sin(incrementalNumber) * 2);
    }
}
