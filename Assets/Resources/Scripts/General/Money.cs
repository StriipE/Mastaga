using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money {

    private float money;

    public Money(float startMoney)
    {
        this.money = startMoney;
    }

    public void addMoney(float money)
    {
        this.money += money;
    }

    public float getMoney()
    {
        return money;
    }
    
    public string toString()
    {
        return money.ToString();
    }
}
