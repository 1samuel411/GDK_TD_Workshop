using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{

    public float destructTime = 5;
    private float destructTimer;


    void Start()
    {
        destructTimer = Time.time + destructTime;
    }

    void Update()
    {
        if (Time.time >= destructTimer)
            Destroy(gameObject);
    }
}
