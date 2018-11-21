using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPylon : Turret
{

    public override void CompleteAction()
    {
        base.CompleteAction();

        AudioManager.instance.PlayClip(AudioManager.Effects.quiet, "EnergyPylon");
        EffectsManager.instance.SpawnEffect("EnergyPylon", transform.position + (Vector3.up * 0.7f));
        PlayerManager.instance.AddMoney(value);
    }

}
