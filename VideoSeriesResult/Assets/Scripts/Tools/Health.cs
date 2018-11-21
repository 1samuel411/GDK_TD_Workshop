using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Image healthBar;
    public float maxHealth;
    public float curHealth;
    public bool isDead
    {
        get
        {
            if (curHealth <= 0)
                return true;

            return false;
        }
    }

    public delegate void OnDeath();
    public OnDeath deadDelegate;

    public delegate void OnDamaged();
    public OnDeath onDamagedDelegate;

    void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = curHealth / maxHealth;
    }

    public void DealDamage(int amount)
    {
        curHealth -= amount;

        if (onDamagedDelegate != null)
            onDamagedDelegate.Invoke();

        if (curHealth <= 0)
        {
            if(deadDelegate != null)
                deadDelegate.Invoke();
        }
    }
}
