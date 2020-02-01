using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDrugConsumable : BaseConsumable
{
    [SerializeField]
    private int m_drugValue;

    [SerializeField]
    private DrugType drugType;

    public enum DrugType
    {
        hp,
        ero,
        food
    }

    protected override void DoAction(IPanda panda)
    {
        switch (drugType)
        {
            case DrugType.ero:
                panda.SetEro(m_drugValue);
                break;

            case DrugType.hp:
                panda.SetHealth(m_drugValue);
                break;

            case DrugType.food:
                panda.SetFood(m_drugValue);
                break;
        }
    }
}