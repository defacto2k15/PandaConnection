using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEroticConsumable : BaseConsumable
{
    [SerializeField]
    private int eroticModifier;
    protected override void DoAction(IPanda panda)
    {
        panda.SetErotic(eroticModifier);
    }
}
