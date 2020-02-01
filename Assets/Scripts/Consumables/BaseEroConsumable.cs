using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEroConsumable : BaseConsumable
{
    [SerializeField]
    private int range;

    [SerializeField]
    private int m_eroNutritionalValue;

    [SerializeField]
    private int timeGivingNutrition;

    protected override void DoAction(IPanda panda)
    {
        panda.SetEro(m_eroNutritionalValue);
    }
}