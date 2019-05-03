using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{

    private static BoatManager _instance;
    public static BoatManager INSTANCE { get { return _instance; } }

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

    public List<GameObject> listOfFishermen = new List<GameObject> { };

    private float timer;
    public float timerInterval = 10.0f;

    public Transform actorPlane;
    public GameObject monkeyPrefab;
    public List<GameObject> monkeys;

    public int fishScore = 0;

    private void Start()
    {
        monkeys = new List<GameObject>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer >= 0)
            return;

        if (monkeys.Count < 4)
            SpawnMonkey();

        timer = timerInterval;
    }

    private void SpawnMonkey()
    {
        Vector3 spawnPos = Waypoints.INSTANCE.GetRandomWaypoint();
        monkeys.Add(Instantiate<GameObject>(monkeyPrefab, spawnPos, Quaternion.identity, actorPlane));
    }
}
