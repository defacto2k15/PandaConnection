using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Stworzone tylko po to zeby przekazywac notyfikacje ze wszelakich modeli do UI. 
/// Generalnie pewnie tak nie powinno sie, ale na naszej malej skali nic zlego sie nie stanie.
/// </summary>
public class NotificationManager : MonoBehaviour
{
    public Action OnMoneyChanged;
    public Action OnResearchUnlocked;
    public Action<IPanda> OnPandaSelected;
    public Action OnShopItemsChanged;
    public Action OnConsumablesChanged;
}
