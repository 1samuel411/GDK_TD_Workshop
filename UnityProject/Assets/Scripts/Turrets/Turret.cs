using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    // Structs
    [System.Serializable]
    public struct Upgrades
    {
        public int cost;
        public int newValue;
        public float newRate;
        public int newRadius;
        public GameObject mesh;
    }

    // Public Variables
    public new string name;
    public Upgrades[] upgrades;
    public Sprite icon;
    public int price;
    public TurretType type;
    [HideInInspector]
    public int value;
    [HideInInspector]
    public float rate;
    [HideInInspector]
    public int radius;
    [HideInInspector]
    public int curUpgrade = 0;

    public bool placed = false;
    public List<Cell> cellsTaken = new List<Cell>();
    public GameObject destroyFx;

    // Private Variables
    private float curTimer;

    public virtual void Update()
    {
        if(placed && Time.time >= curTimer)
        {
            curTimer = Time.time + rate;
            CompleteAction();
        }

        // Update Upgrades
        if (!placed)
            return;

        for(int i = 0; i < upgrades.Length; i++)
        {
            if(i == curUpgrade)
            {
                rate = upgrades[i].newRate;
                value = upgrades[i].newValue;
                radius = upgrades[i].newRadius;
                upgrades[i].mesh.gameObject.SetActive(true);
            }
            else
            {
                upgrades[i].mesh.gameObject.SetActive(false);
            }
        }
    }

    public virtual void CompleteAction() { }

    public void Destroy()
    {
        PlayerManager.instance.AddMoney(GetDestructionValue());

        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "DestroyTurret");
        for(int i = 0; i < cellsTaken.Count; i++)
        {
            cellsTaken[i].SetStatus(Cell.Status.unselected, true);
        }
        Instantiate(destroyFx, transform.position + Vector3.up * 0.75f, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        if (!PlayerManager.instance.CanAfford(GetUpgradeCost()) || curUpgrade >= upgrades.Length - 1)
        {
            AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "CantAfford");
            return;
        }

        curUpgrade += 1;
        price += upgrades[curUpgrade].cost;

        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "Upgrade");
        Instantiate(BuildManager.instance.buildFx, transform.position + Vector3.up * 0.75f, Quaternion.identity);
    }

    private void OnMouseOver()
    {
        Debug.Log("Hover");
        if (BuildManager.instance.editMode)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            // Click on turret
            Debug.Log("Click");
            TurretPanelUI.instance.Show(this);
        }
    }

    public int GetDestructionValue()
    {
        return (int)(price * 0.75f);
    }

    public int GetUpgradeCost()
    {
        return upgrades[curUpgrade].cost;
    }
}

public enum TurretType
{
    Damage, MoneyBoost
}