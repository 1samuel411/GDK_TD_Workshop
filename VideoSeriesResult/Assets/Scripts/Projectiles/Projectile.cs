using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;

    [HideInInspector]
    public Unit target;
    [HideInInspector]
    public int damage;


    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        float magnitude = (transform.position - target.transform.position).magnitude;

        if(magnitude <= 0.5f)
        {
            Destroy(gameObject);
            target.health.DealDamage(damage);
            EffectsManager.instance.SpawnEffect("ImpactEffect", transform.position);
        }

        transform.position -= (transform.position - target.transform.position) * speed * Time.deltaTime;
    }
}
