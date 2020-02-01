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

    public IShopManager shopManager;
    public IPandaManager pandaManager;

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
