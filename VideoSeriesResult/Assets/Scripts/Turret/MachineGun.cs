using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : AttackingTurret
{

    public override void CompleteAction()
    {
        base.CompleteAction();

        if (unitToAttack != null)
        {
            AudioManager.instance.PlayClip(AudioManager.Effects.effects, "MachineGun");
        }
    }

}
