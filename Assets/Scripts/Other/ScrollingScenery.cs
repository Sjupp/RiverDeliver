using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScenery : MonoBehaviour
{
    public GameObject prefab;
    public float speed = 0.1f;

    [Range(0f, 20f)]
    public float depthOffsetForward = 3.0f;
    [Range(0f, 20f)]
    public float depthOffsetBack = 3.0f;    
    public bool dropViaRaycast = false;
    [Range(0.0f, 1.0f)]
    public float occurrence = 0.5f;

    [Header("Can't be changed during runtime")]
    public float dropIntoGroundAmount = 0.0f;

    private Vector3 dropAmount;
    private Vector3 thisPosition;
    private List<GameObject> objectPool;
    private List<SceneryObject> sceneryObjectPool;

    private float timer;

    private void Start()
    {
        dropAmount = new Vector3(0, dropIntoGroundAmount, 0);
        thisPosition = transform.position;
        prefab.SetActive(false);
        objectPool = new List<GameObject>();
        sceneryObjectPool = new List<SceneryObject>();
    }

    private void Update()
    {
        if (timer <= 0)
        {
            if (Random.Range(0.0f, 1.0f) < occurrence)
                SpawnSceneryObject();
            timer = 0.1f;
        }
        timer -= Time.deltaTime;

        foreach (SceneryObject sO in sceneryObjectPool)
        {
            if (sO.isActiveAndEnabled)
            {
                sO.MyUpdate();
            }
        }
        
    }

    private void SpawnSceneryObject()
    {
        Vector3 newPos = new Vector3(thisPosition.x,
                                     thisPosition.y,
                                     thisPosition.z + Random.Range(-depthOffsetForward, depthOffsetBack));
        if (dropViaRaycast)
        {
            if (Physics.Raycast(newPos, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            { }
            newPos = hit.point - dropAmount;
        }

        GameObject go = FetchFromPool(); // Hämta ett objekt ur poolen
        go.transform.SetPositionAndRotation(newPos, Quaternion.Euler(0, Random.Range(0, 360), 0)); // Sätt ut den där jag vill att den ska "spawna"
        go.transform.SetParent(transform);
        go.GetComponent<SceneryObject>().speed = speed; // Sätt objektet i rörelse. I sin egen kod: När den rört sig x långt, SetActive(false);
    }

    public GameObject FetchFromPool()
    {
        foreach (GameObject gameObject in objectPool)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
                return gameObject;
            }
        }

        var newObj = Instantiate<GameObject>(prefab);
        objectPool.Add(newObj);
        sceneryObjectPool.Add(newObj.GetComponent<SceneryObject>());
        return newObj;
    }
}