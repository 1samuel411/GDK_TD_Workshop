using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTank : Unit
{

    public override void Shoot()
    {
        base.Shoot();

        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "Shoot");
    }

}
