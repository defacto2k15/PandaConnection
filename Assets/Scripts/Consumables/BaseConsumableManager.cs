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

    [SerializeField]
    private List<ConsumablePrefabQuantityList> m_startingEroticConsumables = new List<ConsumablePrefabQuantityList>();

    private List<BaseConsumable> m_foodConsumables = new List<BaseConsumable>();
    private List<BaseConsumable> m_drugConsumables = new List<BaseConsumable>();
    private List<BaseConsumable> m_eroticConsumables = new List<BaseConsumable>();

    private void Awake()
    {
        for (int i = 0; i < m_startingFoodConsumables.Count; i++)
        {
            var consumable = m_startingFoodConsumables[i];
            for (int j = 0; j < consumable.quantity; j++)
            {
                m_foodConsumables.Add(GameObject.Instantiate<BaseConsumable>(consumable.consumablePrefab, this.transform));
            }
        }
        for (int i = 0; i < m_startingDrugConsumables.Count; i++)
        {
            var consumable = m_startingDrugConsumables[i];
            for (int j = 0; j < consumable.quantity; j++)
            {
                m_drugConsumables.Add(GameObject.Instantiate<BaseConsumable>(consumable.consumablePrefab, this.transform));
            }
        }
        for (int i = 0; i < m_startingEroticConsumables.Count; i++)
        {
            var consumable = m_startingEroticConsumables[i];
            for (int j = 0; j < consumable.quantity; j++)
            {
                m_eroticConsumables.Add(GameObject.Instantiate<BaseConsumable>(consumable.consumablePrefab, this.transform));
            }
        }
        GameManager.instance.notificationManager.OnConsumablesChanged?.Invoke();
    }

    bool IConsumableManager.Add(IConsumable consumable)
    {
        (consumable as BaseConsumable).transform.SetParent(this.transform);
        if (consumable is BaseFoodConsumable)
        {
            m_foodConsumables.Add(consumable as BaseConsumable);

            GameManager.instance.notificationManager.OnConsumablesChanged?.Invoke();
            return true;
        }
        else if (consumable is BaseDrugConsumable)
        {
            m_drugConsumables.Add(consumable as BaseConsumable);

            GameManager.instance.notificationManager.OnConsumablesChanged?.Invoke();
            return true;
        }
        else
        {
            m_eroticConsumables.Add(consumable as BaseConsumable);

            GameManager.instance.notificationManager.OnConsumablesChanged?.Invoke();
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
        else if (m_eroticConsumables.Contains(baseConsumable))
        {
            m_eroticConsumables.Remove(baseConsumable);
        }
        GameManager.instance.notificationManager.OnConsumablesChanged?.Invoke();
    }

    List<IConsumable> IConsumableManager.GetDrugConsumables()
    {
        return m_drugConsumables.Cast<IConsumable>().ToList();
    }

    List<IConsumable> IConsumableManager.GetFoodConsumables()
    {
        return m_foodConsumables.Cast<IConsumable>().ToList();
    }

    List<IConsumable> IConsumableManager.GetEroticConsumables()
    {
        return m_eroticConsumables.Cast<IConsumable>().ToList();
    }
}