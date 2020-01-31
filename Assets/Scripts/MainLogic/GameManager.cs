using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public IMoneyManager moneyManager;
    public IShopManager shopManager;
    public IPandaManager pandaManager;
}
