using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUIManager : MonoBehaviour
{
    public TMPro.TMP_Text clickedResearchName;
    public TMPro.TMP_Text price;
    public TMPro.TMP_Text desciption;
    public Button buyButton;
    public Button exitButton;
    public Color activeColor;
    public Color unactiveColor;
    public Color boughtColor;

    public void ResearchClicked(IResearchItem clicked)
    {
        desciption.text = clicked.details;
        price.text = "" + clicked.Cost;
        clickedResearchName.text = clicked.researchName;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(clicked.Buy);
        buyButton.interactable = clicked.IsBuyable() && !clicked.Bought;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        exitButton.onClick.AddListener(Resume);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Hide();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}