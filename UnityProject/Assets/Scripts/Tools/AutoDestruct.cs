using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{

    public float timer;
    private float curTimer;

    void Start()
    {
        curTimer = Time.time + timer;
    }

    void Update()
    {
        if(Time.time >= curTimer)
        {
            Destroy(gameObject);
        }
    }
}
