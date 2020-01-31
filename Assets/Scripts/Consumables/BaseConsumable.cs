using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConsumable : MonoBehaviour, IConsumable
{
    void IConsumable.CanConsume(IPanda panda)
    {
        throw new System.NotImplementedException();
    }

    bool IConsumable.CanPlace(Vector2 placementLocation)
    {
        throw new System.NotImplementedException();
    }

    void IConsumable.Consume(IPanda panda)
    {
        throw new System.NotImplementedException();
    }

    Sprite IConsumable.GetIcon()
    {
        throw new System.NotImplementedException();
    }

    string IConsumable.GetName()
    {
        throw new System.NotImplementedException();
    }

    int IConsumable.GetPrice()
    {
        throw new System.NotImplementedException();
    }
}
