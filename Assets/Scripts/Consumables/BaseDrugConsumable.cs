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
                panda.ChangeEro(m_drugValue);
                break;

            case DrugType.hp:
                panda.ChangeHealth(m_drugValue);
                break;

            case DrugType.food:
                panda.ChangeFullness(m_drugValue);
                break;
        }
    }
}