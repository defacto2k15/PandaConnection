using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFoodConsumable : BaseConsumable
{
    [SerializeField]
    public float range;

    [SerializeField]
    private float m_foodNutritionalValue;

    [SerializeField]
    public float timeGivingNutrition;

    protected override void DoAction(IPanda panda)
    {
        panda.ChangeFullness(m_foodNutritionalValue);
    }
}