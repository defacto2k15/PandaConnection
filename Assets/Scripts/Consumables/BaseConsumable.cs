using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

public class BaseConsumable : MonoBehaviour, IConsumable
{

    [SerializeField]
    protected Sprite m_icon;

    [SerializeField]
    protected string m_name;

    [SerializeField]
    protected int m_price;

    bool IConsumable.CanConsume(IPanda panda)
    {
        return true;
    }

    public virtual void PlaceInWorld()
    {

    }

    public virtual bool CanPlace()
    {
        return true;
    }

    void IConsumable.Consume(IPanda panda)
    {
        DoAction(panda);
        GameManager.instance.ConsumableManager.Consume(this);
    }

    protected virtual void DoAction(IPanda panda)
    {
    }

    Sprite IConsumable.GetIcon()
    {
        return m_icon;
    }

    string IConsumable.GetName()
    {
        return m_name;
    }

    int IConsumable.GetPrice()
    {
        return m_price;
    }
}
