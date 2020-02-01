using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ConsumablesListUI : MonoBehaviour
{

    [SerializeField]
    protected BaseConsumableItemUI m_consumablePrefab;

    [SerializeField]
    protected Transform m_foodsParent;

    [SerializeField]
    protected Transform m_drugsParent;

    [SerializeField]
    protected Transform m_eroticsParent;

    protected List<BaseConsumableItemUI> m_spawnedFoods = new List<BaseConsumableItemUI>();

    protected List<BaseConsumableItemUI> m_spawnedDrugs = new List<BaseConsumableItemUI>();

    protected List<BaseConsumableItemUI> m_spawnedErotics = new List<BaseConsumableItemUI>();

    protected virtual void Start()
    {
        GameManager.instance.notificationManager.OnConsumablesChanged += RefreshConsumablesList;
        RefreshConsumablesList();
    }

    protected virtual List<IConsumable> GetDrugConsumables()
    {
        return GameManager.instance.ConsumableManager.GetDrugConsumables();
    }

    protected virtual List<IConsumable> GetFoodConsumables()
    {
        return GameManager.instance.ConsumableManager.GetFoodConsumables();
    }

    protected virtual List<IConsumable> GetEroticConsumables()
    {
        return GameManager.instance.ConsumableManager.GetEroticConsumables(); 
    }

    protected void RefreshConsumablesList()
    {
        var drugConsumables = GetDrugConsumables();
        var foodConsumables = GetFoodConsumables();
        var eroticConsumables = GetEroticConsumables();
        CheckShouldDelete(m_spawnedFoods, foodConsumables);
        CheckShouldDelete(m_spawnedDrugs, drugConsumables);
        CheckShouldDelete(m_spawnedErotics, eroticConsumables);

        CheckShouldAdd(m_spawnedFoods, foodConsumables);
        CheckShouldAdd(m_spawnedDrugs, drugConsumables);
        CheckShouldAdd(m_spawnedErotics, eroticConsumables);
    }

    private void CheckShouldAdd(List<BaseConsumableItemUI> spawnedItems, List<IConsumable> consumablesList)
    {
        for (int i = 0; i < consumablesList.Count; i++)
        {
            if (!spawnedItems.Exists(x => x.Consumable == consumablesList[i]))
            {
                InstantiateUIPrefab(consumablesList[i]);
            }
        }
    }

    private void CheckShouldDelete(List<BaseConsumableItemUI> spawnedItems, List<IConsumable> consumablesList)
    {
        for (int i = spawnedItems.Count - 1; i >= 0; i--)
        {
            if (spawnedItems[i] == null)
            {
                spawnedItems.RemoveAt(i);
            }
            else if (!consumablesList.Contains(spawnedItems[i].Consumable))
            {
                spawnedItems[i].StartConsumeAnimation();
            }
        }
    }

    private void InstantiateUIPrefab(IConsumable consumable)
    {
        var spawnedPrefab = GameObject.Instantiate<BaseConsumableItemUI>(m_consumablePrefab);
        spawnedPrefab.Init(consumable);
        if (consumable is BaseFoodConsumable)
        {
            spawnedPrefab.transform.SetParent(m_foodsParent);
            m_spawnedFoods.Add(spawnedPrefab);
        }
        else if(consumable is BaseDrugConsumable)
        {
            spawnedPrefab.transform.SetParent(m_drugsParent);
            m_spawnedDrugs.Add(spawnedPrefab);
        }
        else
        {
            spawnedPrefab.transform.SetParent(m_eroticsParent);
            m_spawnedErotics.Add(spawnedPrefab);
        }
        spawnedPrefab.transform.localScale = Vector3.one;
    }
}
