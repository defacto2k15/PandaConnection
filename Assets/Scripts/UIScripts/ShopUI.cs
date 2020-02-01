using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopUI : ConsumablesListUI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        GameManager.instance.notificationManager.OnShopItemsChanged += RefreshConsumablesList;
        RefreshConsumablesList();
    }

    protected override List<IConsumable> GetDrugConsumables()
    {
        return GameManager.instance.ShopManager.GetAvailableConsumables().Where(x => x is BaseDrugConsumable).ToList();
    }

    protected override List<IConsumable> GetEroticConsumables()
    {
        return GameManager.instance.ShopManager.GetAvailableConsumables().Where(x => x is BaseEroConsumable).ToList();
    }

    protected override List<IConsumable> GetFoodConsumables()
    {
        return GameManager.instance.ShopManager.GetAvailableConsumables().Where(x => x is BaseFoodConsumable).ToList();
    }
}