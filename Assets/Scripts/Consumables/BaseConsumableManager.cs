using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseConsumableManager : MonoBehaviour, IConsumableManager
{
    [System.Serializable]
    public class ConsumablePrefabQuantityList
    {
        public BaseConsumable consumablePrefab;
        public int quantity;
    }

    [SerializeField]
    private List<ConsumablePrefabQuantityList> m_startingFoodConsumables = new List<ConsumablePrefabQuantityList>();
    [SerializeField]
    private List<ConsumablePrefabQuantityList> m_startingDrugConsumables = new List<ConsumablePrefabQuantityList>();

    private List<BaseConsumable> m_foodConsumables = new List<BaseConsumable>();
    private List<BaseConsumable> m_drugConsumables = new List<BaseConsumable>();

    private void Awake()
    {
        for(int i =0; i<m_startingDrugConsumables.Count; i++)
        {
            var consumable = m_startingFoodConsumables[i];
            for(int j =0; j<consumable.quantity; j++)
            {
                m_foodConsumables.Add(GameObject.Instantiate<BaseConsumable>(consumable.consumablePrefab, this.transform));
            }
        }
    }

    bool IConsumableManager.Add(IConsumable consumable)
    {
        if(consumable is BaseFoodConsumable)
        {
            m_foodConsumables.Add(consumable as BaseConsumable);
            return true;
        }
        else
        {
            m_drugConsumables.Add(consumable as BaseConsumable);
            return true;
        }
        return false;
    }

    void IConsumableManager.Consume(IConsumable consumable)
    {
        var baseConsumable = consumable as BaseConsumable;
        if (m_foodConsumables.Contains(baseConsumable))
        {
            m_foodConsumables.Remove(baseConsumable);
        }
        else if (m_drugConsumables.Contains(baseConsumable))
        {
            m_drugConsumables.Remove(baseConsumable);
        }
        GameManager.instance.notificationManager.OnConsumablesChanged();
    }

    List<IConsumable> IConsumableManager.GetDrugConsumables()
    {
        return m_drugConsumables.Cast<IConsumable>().ToList();
    }

    List<IConsumable> IConsumableManager.GetFoodConsumables()
    {
        return m_foodConsumables.Cast<IConsumable>().ToList();
    }

}
