using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{

    public static BaseManager instance;

    private void Awake()
    {
        // There's already an instance for BaseManager, kill ourselves and the children
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // No instance so lets assign ourselves to it
        instance = this;

        // Dont Destroy
        DontDestroyOnLoad(gameObject);
    }
}
