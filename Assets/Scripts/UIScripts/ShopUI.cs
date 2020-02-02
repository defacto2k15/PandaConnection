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
        consumableRange.SetText($"Range: { consumable.GetRange()}");
        consumablePrice.SetText($"Price: {consumable.GetPrice()}");
        consumableName.SetText(consumable.GetName());
        buyButon.interactable = true;
    }
}