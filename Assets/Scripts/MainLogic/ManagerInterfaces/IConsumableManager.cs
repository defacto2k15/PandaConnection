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

     void Consume(IConsumable consumable);
     bool Add(IConsumable consumable);
}

public interface IConsumable
{
    int GetPrice();
    Sprite GetIcon();
    string GetName();
    bool CanPlace(Vector2 placementLocation);
    void Consume(IPanda panda);
    bool CanConsume(IPanda panda);
}
