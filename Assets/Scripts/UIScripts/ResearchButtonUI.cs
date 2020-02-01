using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResearchButtonUI : MonoBehaviour
{
    private Button button;
    //aktualnie po prostu researchItem dodany do tego obiektu
    private IResearchItem researchItem;

    private void Awake()
    {
        //TODO: jakbysmy spawnowali te drzewka, to research itemy mogly by byc trzymane w zupelnie innym miejscu
        researchItem = GetComponent<IResearchItem>();
        button = GetComponent<Button>();
        button.onClick.AddListener(BuyResearch);
        GameManager.instance.notificationManager.OnMoneyChanged += RecalculateAvailability;
        GameManager.instance.notificationManager.OnResearchUnlocked += RecalculateAvailability;
        RecalculateAvailability();
    }

    private void RecalculateAvailability()
    {
        if (researchItem.Bought)
        {
            button.GetComponent<Image>().color = Color.blue;
        }
        if (researchItem.IsBuyable())
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    private void BuyResearch()
    {
        researchItem.Buy();
    }
}
