using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    public static EffectsManager instance;

    [System.Serializable]
    public struct Effect
    {
        public string name;
        public GameObject obj;
    }
    public Effect[] effects;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void SpawnEffect(string name, Vector3 position)
    {
        Effect effectToSpawn = FindEffect(name);
        Instantiate(effectToSpawn.obj, position, Quaternion.identity);
    }

    Effect FindEffect(string name)
    {
        for(int i = 0; i < effects.Length; i++)
        {
            if(effects[i].name == name)
            {
                return effects[i];
            }
        }

        return new Effect();
    }
}
