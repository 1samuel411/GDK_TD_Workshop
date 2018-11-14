using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{

    public GameObject explosionObj;
    public float attackRate;
    public int attackDamage;
    public float attackDist;
    public int value;

    [HideInInspector]
    public Health health;

    private NavMeshAgent agent;
    private Animator animator;

    private float curAttackTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.dieDelegate += Died;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        agent.SetDestination(HeadQuarters.instance.transform.position);

        if(agent.velocity.magnitude <= 0.5f)
        {
            // Idle
            animator.SetBool("Moving", false);
        }
        else
        {
            // Moving
            animator.SetBool("Moving", true);
        }

        if((transform.position - HeadQuarters.instance.transform.position).magnitude <= attackDist)
        {
            if (Time.time >= curAttackTimer)
            {
                curAttackTimer = Time.time + attackRate;
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        HeadQuarters.instance.health.Damage(attackDamage);
        animator.SetTrigger("Attacking");
    }

    void Died()
    {
        AudioManager.instance.PlayAudio(AudioManager.PlayType.Effects, "UnitExplosion");
        Instantiate(explosionObj, transform.position + Vector3.up * .5f, Quaternion.identity);
        Destroy(gameObject);
        Spawner.instance.spawnedUnits.Remove(this);
        PlayerManager.instance.AddMoney(value);
    }

}
