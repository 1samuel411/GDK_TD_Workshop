using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPylon : Turret
{

    public GameObject actionFx;

    public override void CompleteAction()
    {
        // Add money
        PlayerManager.instance.AddMoney((int)value);
        AudioManager.instance.PlayAudio(AudioManager.PlayType.SilentEffects, "EnergyAdd");
        Instantiate(actionFx, transform.position + Vector3.up * 0.7f, Quaternion.identity);
    }

}
