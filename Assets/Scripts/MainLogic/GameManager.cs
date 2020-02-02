using Assets.Scripts.AI;
using System;
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

    public BaseCrowdManager crowdManager;

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
    public ShopUI shopUIManager;
    public PandaStatisticsUI pandaStatisticsUI;
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

    internal void GoCameraLeft()
    {
        StartCoroutine(MoveCamera(11f));
    }

    internal void GoCameraRight()
    {
        StartCoroutine(MoveCamera(-11f));
    }

    private IEnumerator MoveCamera(float movement)
    {
        var moveTime = 0.95f;
        float timeElapsed = 0;
        var desiredZ = mainCamera.transform.position.z + movement;
        var startingZ = mainCamera.transform.position.z;
        while (timeElapsed < moveTime)
        {
            timeElapsed += Time.deltaTime;
            var newposition = mainCamera.transform.position;
            newposition.z = Mathf.Lerp(startingZ, desiredZ, timeElapsed / moveTime);
            mainCamera.transform.position = newposition;
            yield return null;
        }
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, desiredZ);
    }
}