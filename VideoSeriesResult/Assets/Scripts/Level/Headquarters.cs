using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headquarters : MonoBehaviour
{

    public static Headquarters instance;
    [HideInInspector]
    public Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.deadDelegate += OnDead;
        health.onDamagedDelegate += OnDamaged;
        instance = this;
    }

    public void OnDead()
    {
        GameOverUI.instance.Show();
    }

    public void OnDamaged()
    {
        EffectsManager.instance.SpawnEffect("BaseDamaged", transform.position + (Vector3.up * 0.75f));
        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "BaseDamaged");

    }

}
