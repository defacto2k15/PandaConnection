using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopUI : BaseConsumablesListUI
{
    private IConsumable currentConsumable = null;
    public TMPro.TMP_Text consumableName;
    public TMPro.TMP_Text consumableRange;
    public TMPro.TMP_Text consumablePrice;
    public Button buyButon;
    public Button exitButton;

    // Start is called before the first frame update
    protected override void Start()
    {
        GameManager.instance.notificationManager.OnShopItemsChanged += RefreshConsumablesList;
        RefreshConsumablesList();
        buyButon.onClick.AddListener(BuyConsumable);
    }

    protected override List<IConsumable> GetDrugConsumables()
    {
        return GameManager.instance.ShopManager.GetAvailableConsumables().Where(x => x is BaseDrugConsumable).ToList();
    }

    protected override List<IConsumable> GetEroticConsumables()
    {
        return GameManager.instance.ShopManager.GetAvailableConsumables().Where(x => x is BaseEroConsumable).ToList();
    }

    protected override List<IConsumable> GetFoodConsumables()
    {
        return GameManager.instance.ShopManager.GetAvailableConsumables().Where(x => x is BaseFoodConsumable).ToList();
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

    private void BuyConsumable()
    {
        if (currentConsumable != null && currentConsumable.GetPrice() < GameManager.instance.MoneyManager.GetCurrentMoney())
        {
            GameManager.instance.ShopManager.BuyConsumable(currentConsumable);
        }
    }

    public void ShowDetailsFor(IConsumable consumable)
    {
        currentConsumable = consumable;
        if (consumable as BaseEroConsumable)
        {
            consumableRange.SetText($"Range: { consumable.GetRange()} \n Ero value: {(consumable as BaseEroConsumable).MEroNutritionalValue } \n Time working: {(consumable as BaseEroConsumable).TimeGivingNutrition }");
        }
        else
        if (consumable as BaseFoodConsumable)
        {
            consumableRange.SetText($"Range: { consumable.GetRange()} \n Food value: {(consumable as BaseFoodConsumable).m_foodNutritionalValue } \n Time working: {(consumable as BaseFoodConsumable).timeGivingNutrition }");
        }
        else
        {
            consumableRange.SetText($"Type: {(consumable as BaseDrugConsumable).drugType} \n Potency: { (consumable as BaseDrugConsumable).m_drugValue}");
        }
        consumablePrice.SetText($"Price: {consumable.GetPrice()}");
        consumableName.SetText(consumable.GetName());
        buyButon.interactable = true;
    }
}