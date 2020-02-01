using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDrugConsumable : BaseConsumable
{
    [SerializeField]
    private int m_drugHealingValue;

    protected override void DoAction(IPanda panda)
    {
        panda.SetHealth(m_drugHealingValue);
    }
}
