using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoneyManager 
{
     int GetCurrentMoney();
     int AddMoney(int amount);
     int RemoveMoney(int amount);
}
