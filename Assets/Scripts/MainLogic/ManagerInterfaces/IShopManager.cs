using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopManager 
{
    IConsumable BuyConsumable(IConsumable type);
    void AddConsumable(IConsumable type);
    List<IConsumable> GetAvailableConsumables();
}
