using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseResearchItem : MonoBehaviour, IResearchItem
{
    [SerializeField]
    protected List<BaseResearchItem> m_children;

    public List<IResearchItem> children
    {
        get
        {
            List<IResearchItem> result = new List<IResearchItem>();
            for(int i =0; i<m_children.Count; i++)
            {
                result.Add(m_children[i]);
            }
            return result;
        }
    }

    [SerializeField]
    protected List<BaseResearchItem> m_requirements;

    public List<IResearchItem> requirements
    {
        get
        {
            List<IResearchItem> result = new List<IResearchItem>();
            for (int i = 0; i < m_requirements.Count; i++)
            {
                result.Add(m_requirements[i]);
            }
            return result;
        }
    }

    [SerializeField]
    protected bool m_bought;
    public bool bought {
        get
        {
            return m_bought;
        }
        private set
        {
            m_bought = value;
        }
    }

    [SerializeField]
    protected int m_cost;
    public int cost {
        get
        {
            return m_cost;
        }
    }

    public void Buy()
    {
        GameManager.instance.moneyManager.RemoveMoney(cost);
        bought = true;
        DoEffect();
        //todo: notify UI
    }

    public virtual void DoEffect()
    {

    }

    public virtual bool IsBuyable()
    {
        bool canBuy = true;
        canBuy &= GameManager.instance.moneyManager.GetCurrentMoney() >= cost;
        canBuy &= m_requirements.Exists(x => !x.bought);
        return canBuy;
    }
}
