using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFoodConsumable : BaseConsumable
{
    [SerializeField]
    private int m_foodNutritionalValue;

    protected override void DoAction(IPanda panda)
    {
        panda.SetFood(m_foodNutritionalValue);
    }
}
