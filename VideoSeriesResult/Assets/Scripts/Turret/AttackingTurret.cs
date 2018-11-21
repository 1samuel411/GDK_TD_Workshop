using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingTurret : Turret
{

    public Unit unitToAttack;

    public Transform[] head;

    public Projectile projectile;

    public override void Update()
    {
        base.Update();

        disk.transform.localScale = Vector3.one * 100 * dist;

        if(unitToAttack != null)
            if ((transform.position - unitToAttack.transform.position).magnitude > (dist))
                unitToAttack = null;

        if (unitToAttack == null)
        {
            for (int i = 0; i < WaveManager.instance.spawnedUnits.Count; i++)
            {
                float magnitude = (transform.position - WaveManager.instance.spawnedUnits[i].transform.position).magnitude;

                if (magnitude <= (dist))
                {
                    // can attack
                    unitToAttack = WaveManager.instance.spawnedUnits[i];
                }
            }
        }

        if(unitToAttack != null)
        {
            for(int i = 0; i < head.Length; i++)
                head[i].transform.LookAt(unitToAttack.transform);
        }
    }

    public override void CompleteAction()
    {
        base.CompleteAction();

        if(unitToAttack != null)
        {
            GameObject newProjectileObj = Instantiate(projectile.gameObject, head[0].transform.position, head[0].transform.rotation);

            Projectile newProjectile = newProjectileObj.GetComponent<Projectile>();
            newProjectile.damage = value;
            newProjectile.target = unitToAttack;
        }
    }

}
