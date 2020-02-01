using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private BaseMoneyManager m_moneyManager;

    public IMoneyManager MoneyManager
    {
        get
        {
            return m_moneyManager;
        }
    }

    [SerializeField]
    private BaseConsumableManager m_consumableManager;

    public IConsumableManager ConsumableManager
    {
        get
        {
            return m_consumableManager;
        }
    }

    [SerializeField]
    private BaseShopManager m_shopManager;

    public IShopManager ShopManager
    {
        get
        {
            return m_shopManager;
        }
    }

    public IPandaManager pandaManager;
    public ResearchUIManager researchUIManager;
    public NotificationManager notificationManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}