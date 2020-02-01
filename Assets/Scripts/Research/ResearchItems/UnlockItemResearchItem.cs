using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockItemResearchItem : BaseResearchItem
{
    public BaseConsumable itemPrefab;

    public override void DoEffect()
    {
        GameManager.instance.ShopManager.AddConsumable(itemPrefab);
    }
}