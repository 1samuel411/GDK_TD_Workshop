using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [System.Serializable]
    public struct UpgradeStruct
    {
        public int price;
        public float newRate;
        public int newValue;
        public float newDist;
        public GameObject turretObj;
    }
    public UpgradeStruct[] upgrades;
    public int curUpgrade;

    public float rate;
    public int radius;
    public int value;
    public float dist;

    public string turretName;
    public int price;
    public Sprite icon;

    public GameObject disk;

    [HideInInspector]
    public bool placed;
    [HideInInspector]
    public Cell[] cellsPlacedOn;

    private float timer;

    public virtual void Update()
    {
        if(Time.time >= timer && placed)
        {
            timer = Time.time + rate;
            CompleteAction();
        }
    }

    public int GetTurretPrice()
    {
        return (int)(price * 0.75f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        PlayerManager.instance.AddMoney(GetTurretPrice());
        Grid.instance.UnmarkUsed(cellsPlacedOn);
        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "TurretDestroy");
        EffectsManager.instance.SpawnEffect("Destroy", transform.position + Vector3.up * 0.4f);
    }

    public void Upgrade()
    {
        if(curUpgrade >= upgrades.Length - 1)
        {
            return;
        }
        if (!PlayerManager.instance.CanAfford(upgrades[curUpgrade + 1].price))
        {
            AudioManager.instance.PlayClip(AudioManager.Effects.effects, "CantAfford");
            return;
        }

        curUpgrade++;
        PlayerManager.instance.Buy(upgrades[curUpgrade].price);
        
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].turretObj.gameObject.SetActive(false);
        }
        upgrades[curUpgrade].turretObj.gameObject.SetActive(true);

        rate = upgrades[curUpgrade].newRate;
        dist = upgrades[curUpgrade].newDist;
        value = upgrades[curUpgrade].newValue;
        price += upgrades[curUpgrade].price;
        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "Upgrade");
        EffectsManager.instance.SpawnEffect("Place", transform.position + (Vector3.up * 0.4f));
    }

    private void OnMouseDown()
    {
        if(placed)
            TurretDataUI.instance.Show(this);
    }


    public virtual void CompleteAction()
    {
        Debug.Log("Complete Action");
    }

}
