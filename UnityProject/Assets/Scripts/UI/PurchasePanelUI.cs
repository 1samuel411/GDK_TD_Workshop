using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasePanelUI : MonoBehaviour
{

    // Public Variables
    public GameObject turretUIObj;
    public Transform contentHolder;

    // Private Variables
    private List<TurretUI> turretUI = new List<TurretUI>();
    private float originalPos;

    void Start()
    {
        // Initialize
        Initialize();

        originalPos = transform.position.y;
    }

    void Initialize()
    {
        for(int i = 0; i < BuildManager.instance.turretObjects.Length; i++)
        {
            // Spawn a UI Object
            TurretUI newTurretUI = SpawnTurretUIObj();
            newTurretUI.turret = BuildManager.instance.turretObjects[i];
            turretUI.Add(newTurretUI);
        }
    }

    void Update()
    {
        // Check if we are in edit mode
        float targetY = BuildManager.instance.editMode ? -originalPos : originalPos;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), 5 * Time.deltaTime);
    }

    TurretUI SpawnTurretUIObj()
    {
        GameObject newObj = Instantiate(turretUIObj);
        newObj.transform.SetParent(contentHolder);
        newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localScale = Vector3.one;
        TurretUI turretUI = newObj.GetComponent<TurretUI>();
        return turretUI;
    }
}
