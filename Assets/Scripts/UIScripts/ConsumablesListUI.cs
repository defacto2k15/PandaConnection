using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConsumablesListUI : MonoBehaviour
{
    [SerializeField]
    private ConsumableItemUI m_consumablePrefab;

    [SerializeField]
    private Transform m_consumablesParent;

    private List<ConsumableItemUI> m_spawnedPrefabs = new List<ConsumableItemUI>();

    private void Awake()
    {
        GameManager.instance.notificationManager.OnConsumablesChanged += RefreshConsumablesList;
    }

    private void RefreshConsumablesList()
    {
        var drugConsumables = GameManager.instance.ConsumableManager.GetDrugConsumables();
        var foodConsumables = GameManager.instance.ConsumableManager.GetFoodConsumables();
        for (int i = m_spawnedPrefabs.Count - 1; i >= 0; i--)
        {
            if (m_spawnedPrefabs[i] == null)
            {
                m_spawnedPrefabs.RemoveAt(i);
            }
            else if (!drugConsumables.Contains(m_spawnedPrefabs[i].consumable) && !foodConsumables.Contains(m_spawnedPrefabs[i].consumable))
            {
                m_spawnedPrefabs[i].StartConsumeAnimation();
            }
        }

        for (int i = 0; i < drugConsumables.Count; i++)
        {
            if (!m_spawnedPrefabs.Exists(x => x.consumable == drugConsumables[i]))
            {
                InstantiateUIPrefab(drugConsumables[i]);
            }
        }

        for(int i=0; i<foodConsumables.Count; i++)
        {
            if (!m_spawnedPrefabs.Exists(x => x.consumable == foodConsumables[i]))
            {
                InstantiateUIPrefab(foodConsumables[i]);
            }
        }
    }

    private void InstantiateUIPrefab(IConsumable consumable)
    {
        var spawnedPrefab = GameObject.Instantiate<ConsumableItemUI>(m_consumablePrefab);
        spawnedPrefab.consumable = consumable;
        spawnedPrefab.transform.SetParent(m_consumablesParent);
        m_spawnedPrefabs.Add(spawnedPrefab);
    }
}
