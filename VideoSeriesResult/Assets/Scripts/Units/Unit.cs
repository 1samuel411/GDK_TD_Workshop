using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    [HideInInspector]
    public Health health;

    public float shootDist;
    public float shootRate;
    public int damage;

    private float shootTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        health.deadDelegate += OnDead;
    }

    private void Start()
    {

    }

    private void Update()
    {
        agent.SetDestination(Headquarters.instance.transform.position);

        float magnitude = (transform.position - Headquarters.instance.transform.position).magnitude;

        if (magnitude <= shootDist)
        {
            if(Time.time >= shootTimer)
            {
                shootTimer = Time.time + shootRate;
                Shoot();
            }
        }

        if(agent.velocity.magnitude <= 0.5f)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }
    }

    public virtual void Shoot()
    {
        animator.SetTrigger("Attack");
        Headquarters.instance.health.DealDamage(damage);
    }

    public void OnDead()
    {
        Destroy(gameObject);
        PlayerManager.instance.AddMoney(5);
        AudioManager.instance.PlayClip(AudioManager.Effects.effects, "Died");
        EffectsManager.instance.SpawnEffect("Died", transform.position + Vector3.up * 0.5f);
        WaveManager.instance.spawnedUnits.Remove(this);
    }

}
