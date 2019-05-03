using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private static Waypoints _instance;
    public static Waypoints INSTANCE { get { return _instance; } }

    private int previousNumber = 0;
    private int number = 0;
    public List<Transform> waypoints = new List<Transform> {  };
    public List<Pickup> fishBoxPickups = new List<Pickup>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Vector3 GetRandomWaypoint()
    {
        bool hej = true;

        while (hej)
        {
            number = Random.Range(0, waypoints.Count - 1);
            if (number != previousNumber)
            {
                hej = false;
            }
        }
        previousNumber = number;
        return waypoints[number].position;
    }

    public Vector3 GetAvailableFishBox()
    {
        var availableBoxes = new List<Vector3>();
        for (int i = 0; i < fishBoxPickups.Count; i++)
        {
            if (fishBoxPickups.Count <= 0)
                return Vector3.zero;

            var pickUp = fishBoxPickups[i];
            if (pickUp.isStored && !pickUp.isBeingCarried && !pickUp.storedLocation.attachedToExtractor)
                availableBoxes.Add(pickUp.transform.position);
        }
        if (availableBoxes.Count == 0)
        {
            return Vector3.zero;
        }
        else
        {
            //return availableBoxes[Random.Range((int)0, (int)fishBoxPickups.Count - 1)];
            return availableBoxes[0];
        }

    }
}
