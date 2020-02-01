using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShopManager : MonoBehaviour, IShopManager
{
    [SerializeField]
    private List<BaseConsumable> m_startingConsumables = new List<BaseConsumable>();

    private List<IConsumable> m_consumables = new List<IConsumable>();

    private void Awake()
    {
        m_consumables.AddRange(m_startingConsumables);
    }

    void IShopManager.AddConsumable(IConsumable type)
    {
        m_consumables.Add(type);
        GameManager.instance.notificationManager.OnShopItemsChanged?.Invoke();
    }

    IConsumable IShopManager.BuyConsumable(IConsumable type)
    {
        GameManager.instance.MoneyManager.RemoveMoney(type.GetPrice());
        var baseConsumable = type as BaseConsumable;
        var instance = GameObject.Instantiate<BaseConsumable>(baseConsumable) as IConsumable;
        GameManager.instance.ConsumableManager.Add(instance);
        return instance;
    }

    List<IConsumable> IShopManager.GetAvailableConsumables()
    {
        return m_consumables;
    }
}