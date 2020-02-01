using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoneyManager : MonoBehaviour, IMoneyManager
{
    [SerializeField]
    private int m_money;

    public int Money
    {
        get
        {
            return m_money;
        }
        private set
        {
            m_money = value;
            GameManager.instance.notificationManager.OnMoneyChanged();
        }
    }

    int IMoneyManager.AddMoney(int amount)
    {
        Money += amount;
        return Money;
    }

    int IMoneyManager.GetCurrentMoney()
    {
        return Money;
    }

    int IMoneyManager.RemoveMoney(int amount)
    {
        Money -= amount;
        return Money;
    }
}
