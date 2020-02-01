using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemUI : MonoBehaviour
{
    public IConsumable consumable;

    public void Awake()
    {
        
    }

    public void StartConsumeAnimation()
    {
        Destroy(this.gameObject);
    }
}
