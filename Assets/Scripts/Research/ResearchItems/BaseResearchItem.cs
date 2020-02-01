using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseResearchItem : MonoBehaviour, IResearchItem
{
    [SerializeField]
    protected List<BaseResearchItem> m_requirements;

    public List<IResearchItem> Requirements
    {
        get
        {
            return m_requirements.Cast<IResearchItem>().ToList();
        }
    }

    [SerializeField]
    protected bool m_bought;

    public bool Bought
    {
        get
        {
            return m_bought;
        }
        private set
        {
            m_bought = value;
            GameManager.instance.notificationManager.OnResearchUnlocked();
        }
    }

    [SerializeField]
    protected int m_cost;

    public int Cost
    {
        get
        {
            return m_cost;
        }
    }

    [SerializeField]
    protected string m_details;

    public string details
    {
        get
        {
            return m_details;
        }
    }

    [SerializeField]
    protected string m_researchName;

    public string researchName
    {
        get
        {
            return m_researchName;
        }
    }

    public void Buy()
    {
        GameManager.instance.MoneyManager.RemoveMoney(Cost);
        Bought = true;
        GameManager.instance.researchUIManager.buyButton.interactable = false;
        DoEffect();
        //todo: notify UI
    }

    public virtual void DoEffect()
    {
    }

    public virtual bool IsBuyable()
    {
        bool canBuy = true;
        canBuy &= !Bought;
        int currentMoney = GameManager.instance.MoneyManager.GetCurrentMoney();
        canBuy &= currentMoney >= Cost;
        canBuy &= (m_requirements == null || !m_requirements.Exists(x => !x.Bought));
        return canBuy;
    }
}