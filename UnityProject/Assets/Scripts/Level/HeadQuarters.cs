using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuarters : MonoBehaviour
{

    public static HeadQuarters instance;

    [HideInInspector]
    public Health health;

    public GameObject damagedEffect;

    void Awake()
    {
        instance = this;

        health = GetComponent<Health>();
        health.dieDelegate += Die;
        health.dmgDelegate += Dmg;
    }

    void Die()
    {
        GameOverPanelUI.instance.Show();
    }

    void Dmg(int dmg)
    {
        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "HeadQuartersDamaged");
        Instantiate(damagedEffect, transform.position, Quaternion.identity);
    }
    
    void Update()
    {

    }
}
