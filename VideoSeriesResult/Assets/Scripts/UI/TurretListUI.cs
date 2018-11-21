using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretListUI : MonoBehaviour
{

    public Transform contentHolder;
    public GameObject uiObjPrefab;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for(int i = 0; i < TurretManager.instance.turrets.Length; i++)
        {
            GameObject newObj = Instantiate(uiObjPrefab, contentHolder);
            newObj.transform.localScale = Vector3.one;
            newObj.transform.rotation = Quaternion.identity;
            newObj.gameObject.GetComponent<TurretObjUI>().turret = TurretManager.instance.turrets[i];
        }
    }
}
