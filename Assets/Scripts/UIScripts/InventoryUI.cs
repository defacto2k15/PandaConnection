using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    protected InventoryConsumableItemUI m_consumablePrefab;

    [SerializeField]
    protected Transform m_foodsParent;

    [SerializeField]
    protected Transform m_drugsParent;

    [SerializeField]
    protected Transform m_eroticsParent;

    protected List<InventoryConsumableItemUI> m_spawnedFoods = new List<InventoryConsumableItemUI>();

    protected List<InventoryConsumableItemUI> m_spawnedDrugs = new List<InventoryConsumableItemUI>();

    protected List<InventoryConsumableItemUI> m_spawnedErotics = new List<InventoryConsumableItemUI>();

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

    private void CheckShouldAdd(List<InventoryConsumableItemUI> spawnedItems, List<IConsumable> consumablesList)
    {
        for (int i = 0; i < consumablesList.Count; i++)
        {
            if (!spawnedItems.Exists(x => x.Consumable == consumablesList[i]))
            {
                InstantiateUIPrefab(consumablesList[i]);
            }
        }
    }

    private void CheckShouldDelete(List<InventoryConsumableItemUI> spawnedItems, List<IConsumable> consumablesList)
    {
        for (int i = spawnedItems.Count - 1; i >= 0; i--)
        {
            if (spawnedItems[i] == null)
            {
                spawnedItems.RemoveAt(i);
            }
            else if (!consumablesList.Exists(x=>x.GetName()==spawnedItems[i].Consumable.GetName()))
            {
                spawnedItems[i].StartConsumeAnimation();
            }
        }
    }

    private void InstantiateUIPrefab(IConsumable consumable)
    {
        bool found = false;
        InventoryConsumableItemUI foundObject = null;
        if (consumable is BaseFoodConsumable)
        {
            foundObject = m_spawnedFoods.Find(o => o.Consumable.GetName() == consumable.GetName());
            if (foundObject != null)
            {
                found = true;
                foundObject.count = GameManager.instance.ConsumableManager.GetFoodConsumables().FindAll(o => o.GetName() == consumable.GetName()).Count;
            }
        }
        else if (consumable is BaseDrugConsumable)
        {
            foundObject = m_spawnedDrugs.Find(o => o.Consumable.GetName() == consumable.GetName());
            if (foundObject != null)
            {
                found = true;
                foundObject.count = GameManager.instance.ConsumableManager.GetDrugConsumables().FindAll(o => o.GetName() == consumable.GetName()).Count;
            }
        }
        else
        {
            foundObject = m_spawnedErotics.Find(o => o.Consumable.GetName() == consumable.GetName());
            if (foundObject != null)
            {
                found = true;
                foundObject.count = GameManager.instance.ConsumableManager.GetEroticConsumables().FindAll(o => o.GetName() == consumable.GetName()).Count;
            }
        }
        if (found == false)
        {
            var spawnedPrefab = GameObject.Instantiate<InventoryConsumableItemUI>(m_consumablePrefab);
            spawnedPrefab.Init(consumable);
            if (consumable is BaseFoodConsumable)
            {
                spawnedPrefab.transform.SetParent(m_foodsParent);
                m_spawnedFoods.Add(spawnedPrefab);
            }
            else if (consumable is BaseDrugConsumable)
            {
                spawnedPrefab.transform.SetParent(m_drugsParent);
                m_spawnedDrugs.Add(spawnedPrefab);
            }
            else
            {
                spawnedPrefab.transform.SetParent(m_eroticsParent);
                m_spawnedErotics.Add(spawnedPrefab);
            }
        }
        else
        {
            foundObject.RefreshUI();
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}