using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFoodConsumable : BaseConsumable
{
    [SerializeField]
    public int range;

    [SerializeField]
    private int m_foodNutritionalValue;

    [SerializeField]
    public int timeGivingNutrition;

    protected override void DoAction(IPanda panda)
    {
        panda.ChangeFullness(m_foodNutritionalValue);
    }
}