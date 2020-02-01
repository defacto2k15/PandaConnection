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
    List<IConsumable> GetEroticConsumables();

    void Consume(IConsumable consumable);
     bool Add(IConsumable consumable);
}

public interface IConsumable
{
    int GetPrice();
    Sprite GetIcon();
    string GetName();
    bool CanPlace();
    void PlaceInWorld();
    void Consume(IPanda panda);
    bool CanConsume(IPanda panda);
}
