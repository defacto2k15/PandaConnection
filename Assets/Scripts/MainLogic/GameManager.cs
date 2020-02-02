using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera _mainCamera;

    public Camera mainCamera
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }
    public LayerMask foodPileMask;
    public LayerMask floorMask;
    public LayerMask pandaMask;

    public FoodPile foodPilePrefab;
    public EroPileActivityArea eroPilePrefab;

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

    public BasePandaManager pandaManager;
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