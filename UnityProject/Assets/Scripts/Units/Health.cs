using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Public Variables
    public bool dead;
    public int health;
    private int curHealth;

    // UI
    public Image healthBar;

    // Delegates
    public delegate void DieDelegate();
    public DieDelegate dieDelegate;
    public delegate void DmgDelegate(int dmg);
    public DmgDelegate dmgDelegate;

    private void Awake()
    {
        curHealth = health;
    }

    private void Update()
    {
        if(curHealth <= 0 && !dead)
        {
            Die();
        }

        if(healthBar)
            healthBar.fillAmount = curHealth / (float)health;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
        curHealth = newHealth;
    }

    void Die()
    {
        dead = true;
        if (dieDelegate != null)
            dieDelegate.Invoke();
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;

        if (dmgDelegate != null)
            dmgDelegate.Invoke(dmg);
    }

}
