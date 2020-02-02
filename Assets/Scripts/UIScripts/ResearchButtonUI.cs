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
    ResearchUIManager manager;

    private void Awake()
    {
        //TODO: jakbysmy spawnowali te drzewka, to research itemy mogly by byc trzymane w zupelnie innym miejscu
        researchItem = GetComponent<IResearchItem>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowDetails);
        manager = GameManager.instance.researchUIManager;
        GameManager.instance.notificationManager.OnMoneyChanged += RecalculateAvailability;
        GameManager.instance.notificationManager.OnResearchUnlocked += RecalculateAvailability;
        RecalculateAvailability();
    }

    private void RecalculateAvailability()
    {
        if (researchItem.Bought)
        {
            button.GetComponent<Image>().color = manager.boughtColor;
        }
        else
        {
            if (researchItem.IsBuyable())
            {
                button.GetComponent<Image>().color = manager.activeColor;
            }
            else
            {
                button.GetComponent<Image>().color = manager.unactiveColor;
            }
        }
    }

    private void ShowDetails()
    {
        GameManager.instance.researchUIManager.ResearchClicked(researchItem);
    }
}