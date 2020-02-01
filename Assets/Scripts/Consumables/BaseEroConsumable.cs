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
        panda.ChangeEro(m_eroNutritionalValue);
    }

    public int Range => range;

    public int MEroNutritionalValue => m_eroNutritionalValue;

    public int TimeGivingNutrition => timeGivingNutrition;
}