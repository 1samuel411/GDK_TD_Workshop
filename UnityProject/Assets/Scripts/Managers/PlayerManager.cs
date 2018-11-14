using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    // Static instance
    public static PlayerManager instance;

    // Public variables
    public int startMoney;

    // Private variables
    private int money;

    private void Awake()
    {
        money = startMoney;
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public int GetMoney()
    {
        return money;
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
    }

    public bool CanAfford(int amount)
    {
        if (money - amount >= 0)
            return true;

        return false;
    }
}
