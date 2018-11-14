using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject impactEffect;
    public float speed;
    public string impactSound;

    [HideInInspector]
    public Unit targetUnit;
    [HideInInspector]
    public int damage;

    void Start()
    {

    }

    private bool closeEnough;
    private float destroyTimer;
    void Update()
    {
        if (closeEnough)
        {
            if (Time.time >= destroyTimer)
                Destroy(gameObject);
            return;
        }

        if (targetUnit == null)
            return;

        transform.position += (targetUnit.transform.position - transform.position) * speed * Time.deltaTime;

        if((transform.position - targetUnit.transform.position).magnitude <= 2)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            targetUnit.health.Damage(damage);
            closeEnough = true;
            destroyTimer = Time.time + destroyTimer;
            AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, impactSound);
        }
    }

}
