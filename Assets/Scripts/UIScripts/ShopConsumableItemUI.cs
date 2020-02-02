using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Sounds;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopConsumableItemUI : BaseConsumableItemUI
{
    public TMPro.TMP_Text price;

    private void Start()
    {
        GameManager.instance.notificationManager.OnMoneyChanged += RecalculateAvailability;
        RecalculateAvailability();
    }

    public override void Init(IConsumable consumable)
    {
        base.Init(consumable);
        price.text = consumable.GetPrice() + "";
    }

    private void RecalculateAvailability()
    {
        m_button.interactable = GameManager.instance.MoneyManager.GetCurrentMoney() >= Consumable.GetPrice();
    }

    public override void DoAction()
    {
        SoundManager.instance.PlayOneShotSound(SoundType.MenuClick);
        this.GetComponentInParent<ShopUI>().ShowDetailsFor(Consumable);
    }
}