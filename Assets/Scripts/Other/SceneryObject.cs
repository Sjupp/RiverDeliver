using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryObject : MonoBehaviour
{
    public float speed = 1;

    public void MyUpdate()
    {
        if (gameObject.transform.position.x > 100)
            gameObject.SetActive(false);
        else
            transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
