﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFoodConsumable : BaseConsumable
{
    [SerializeField]
    private int range;

    [SerializeField]
    private int m_foodNutritionalValue;

    [SerializeField]
    private int timeGivingNutrition;

    protected override void DoAction(IPanda panda)
    {
        panda.SetFood(m_foodNutritionalValue);
    }
}