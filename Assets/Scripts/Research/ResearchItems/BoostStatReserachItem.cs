using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostStatReserachItem : BaseResearchItem
{
    public int value;

    public enum StatType
    {
        maxHealth,
        maxEro,
        maxFood,
        chanceForInfection,
        cashMultiplier
    }

    public StatType statType;

    public override void DoEffect()
    {
        switch (statType)
        {
            case StatType.cashMultiplier:
                GameManager.instance.pandaManager.ChangeCashMultiplier(value);
                break;

            case StatType.maxHealth:
                GameManager.instance.pandaManager.BoostHealth(value);
                break;

            case StatType.chanceForInfection:
                GameManager.instance.pandaManager.ChangeInfectionChange(value);
                break;

            case StatType.maxFood:
                GameManager.instance.pandaManager.BoostMaxFood(value);
                break;

            case StatType.maxEro:
                GameManager.instance.pandaManager.BoostMaxEro(value);
                break;
        }
    }
}