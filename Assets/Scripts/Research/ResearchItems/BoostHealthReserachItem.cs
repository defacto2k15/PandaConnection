﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostStatReserachItem : BaseResearchItem
{
    public int healthBoost;

    public override void DoEffect()
    {
        GameManager.instance.pandaManager.BoostHealth(healthBoost);
    }
}