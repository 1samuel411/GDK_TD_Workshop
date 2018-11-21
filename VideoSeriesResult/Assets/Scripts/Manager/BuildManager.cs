using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    public bool editMode;
    private Turret turretToPlace;

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

    private void Update()
    {
        if (editMode == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Place
                Place();
            }

            if (Input.GetMouseButtonDown(1))
            {
                // Cancel
                Cancel();
            }

            if(turretToPlace != null && Grid.instance.selectedCells.Length >= Mathf.Pow(turretToPlace.radius, 2))
            {
                // Average position of all selected Cells
                Vector3 position = Vector3.zero;
                for(int i = 0; i < Grid.instance.selectedCells.Length; i++)
                {
                    position += Grid.instance.selectedCells[i].transform.position;
                }
                position /= Grid.instance.selectedCells.Length;
                turretToPlace.transform.position = position;
            }
        }
    }

    void Place()
    {
        if (Grid.instance.selectedCells.Length < Mathf.Pow(turretToPlace.radius, 2))
            return;

        editMode = false;

        PlayerManager.instance.Buy(turretToPlace.price);

        turretToPlace.placed = true;
        Grid.instance.MarkUsed(Grid.instance.selectedCells);
        turretToPlace.cellsPlacedOn = Grid.instance.selectedCells;
        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "TurretPlace");
        EffectsManager.instance.SpawnEffect("Place", turretToPlace.transform.position + Vector3.up * 0.4f);

        turretToPlace = null;
    }

    void Cancel()
    {
        editMode = false;
        Destroy(turretToPlace.gameObject);
        turretToPlace = null;
    }

    public void BuildTurret(Turret turret)
    {
        if (PlayerManager.instance.CanAfford(turret.price) == false)
        {
            AudioManager.instance.PlayClip(AudioManager.Effects.effects, "CantAfford");
            return;
        }
        if (editMode)
            return;

        // Spawn the turret
        GameObject newObj = Instantiate(turret.gameObject);
        turretToPlace = newObj.GetComponent<Turret>();
        Grid.instance.radius = turretToPlace.radius;

        editMode = true;
    }

}
