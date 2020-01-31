using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To jest nasze inventory - zwraca listy zakupionych przez nas produktow, umozliwia
/// </summary>
public interface IConsumableManager 
{
    List<IConsumable> GetFoodConsumables();
    List<IConsumable> GetDrugConsumables();

    IConsumable ReturnConsumable();
     bool AddConsumable(IConsumable consumable);
}

public interface IConsumable
{
    int GetPrice();
    Sprite GetIcon();
    string GetName();
    bool CanPlace(Vector2 placementLocation);
    void Consume(IPanda panda);
    void CanConsume(IPanda panda);
}
