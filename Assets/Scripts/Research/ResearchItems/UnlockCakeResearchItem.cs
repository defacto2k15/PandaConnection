using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockConsumableResearchItem : BaseResearchItem
{
    public BaseConsumable itemPrefab;
    public override void DoEffect()
    {
        GameManager.instance.ShopManager.AddConsumable(itemPrefab);
    }
}
