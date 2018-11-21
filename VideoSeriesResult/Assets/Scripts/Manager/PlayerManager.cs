using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public int money;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void Buy(int cost)
    {
        money -= cost;
    }

    public bool CanAfford(int cost)
    {
        if (money < cost)
            return false;

        return true;
    }

}
