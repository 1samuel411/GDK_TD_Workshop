using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    // Static Varaibles
    public static BuildManager instance;

    // Public Variables
    public Turret[] turretObjects;
    public bool editMode;
    public GameObject buildFx;

    // Private Variables
    public Turret turretToBuild;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (editMode)
            UpdateEditMode();
    }

    void UpdateEditMode()
    {
        if(Grid.instance.canPlace)
            turretToBuild.transform.position = Grid.instance.position;

        if (Grid.instance.initialPositionValid == false)
            turretToBuild.transform.position = Vector3.up * 1000;

        if (Input.GetMouseButtonDown(0) && Grid.instance.initialPositionValid)
        {
            Place();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Cancel();
        }
    }

    void Cancel()
    {
        editMode = false;
        Destroy(turretToBuild.gameObject);
        turretToBuild = null;
    }

    void Place()
    {
        Instantiate(buildFx, turretToBuild.transform.position + Vector3.up * 1, Quaternion.identity);
        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "PlaceTurret");
        turretToBuild.placed = true;
        Grid.instance.Place();

        // Remove money
        PlayerManager.instance.RemoveMoney(turretToBuild.price);

        editMode = false;
        turretToBuild = null;
    }

    public void EnterBuildMode(Turret turret)
    {
        editMode = true;

        // Spawn a turret for editing
        GameObject newObj = Instantiate(turret.gameObject);
        turretToBuild = newObj.GetComponent<Turret>();
    }

}
