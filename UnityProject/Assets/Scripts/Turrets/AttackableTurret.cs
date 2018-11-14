using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableTurret : Turret
{

    public Transform[] turretPointers;
    public Transform circle;
    public Projectile projectile;
    public float range;
    public string attackSound;

    private Unit targetUnit;

    private Transform turretPointer
    {
        get
        {
            for(int i =0; i < turretPointers.Length; i++)
            {
                if (turretPointers[i].gameObject.activeInHierarchy)
                    return turretPointers[i];
            }

            return null;
        }
    }

    public override void Update()
    {
        base.Update();

        if(TurretPanelUI.instance.turret == this)
        {
            circle.gameObject.SetActive(true);
        }
        else
        {
            if (placed)
                circle.gameObject.SetActive(false);
        }

        circle.transform.localScale = Vector3.one * range * 100;

        if (!placed)
            return;

        if (targetUnit != null)
        {
            turretPointer.transform.LookAt(targetUnit.transform);

            // Check for range
            if ((transform.position - targetUnit.transform.position).magnitude > (range + 1))
                targetUnit = null;
        }
        else
        {
            // Find target
            for(int i = 0; i < Spawner.instance.spawnedUnits.Count; i++)
            {
                if((transform.position - Spawner.instance.spawnedUnits[i].transform.position).magnitude <= (range + 1))
                {
                    targetUnit = Spawner.instance.spawnedUnits[i];
                }
            }
        }
    }

    public override void CompleteAction()
    {
        base.CompleteAction();

        if (targetUnit == null)
            return;

        // Spawn projectile
        GameObject newObj = Instantiate(projectile.gameObject, turretPointer.position, turretPointer.rotation);
        Projectile newProjectile = newObj.GetComponent<Projectile>();
        newProjectile.targetUnit = targetUnit;
        newProjectile.damage = value;

        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, attackSound);
    }

}
