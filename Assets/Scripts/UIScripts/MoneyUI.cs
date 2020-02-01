using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TMPro.TMP_Text moneyText;

    private void Awake()
    {
        OnMoneyChanged();
        GameManager.instance.notificationManager.OnMoneyChanged += OnMoneyChanged;
    }

    private void OnMoneyChanged()
    {
        moneyText.SetText(GameManager.instance.MoneyManager.GetCurrentMoney().ToString());
        //TODO: animacje?
    }
}
