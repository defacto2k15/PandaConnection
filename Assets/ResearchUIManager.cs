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

    public void ResearchClicked(IResearchItem clicked)
    {
        desciption.text = clicked.details;
        price.text = "" + clicked.Cost;
        clickedResearchName.text = clicked.researchName;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(clicked.Buy);
        buyButton.interactable = clicked.IsBuyable() && !clicked.Bought;
    }
}