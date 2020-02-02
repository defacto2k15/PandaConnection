using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDrugConsumable : BaseConsumable
{
    [SerializeField]
    public int m_drugValue;

    public Color drugColor;

    [SerializeField]
    public DrugType drugType;

    public enum DrugType
    {
        hp,
        ero,
        food
    }

    public override void DoAction(IPanda panda)
    {
        switch (drugType)
        {
            case DrugType.ero:
                panda.ChangeEro(m_drugValue);
                break;

            case DrugType.hp:
                panda.ChangeHealth(m_drugValue);
                break;

            case DrugType.food:
                panda.ChangeFullness(m_drugValue);
                break;
        }
    }

    public override bool CanPlace()
    {
        var foodPile = Util.RaycastFoodPile();
        return (foodPile != null && foodPile.Drug == null);
    }

    public override void PlaceInWorld()
    {
        var foodPile = Util.RaycastFoodPile();
        if (foodPile == null || foodPile.Drug != null)
        {
            Debug.LogError("My method failed - no foodpile to place or drug already on pile");
            return;
        }
        foodPile.Drug = this;
        foodPile.foodSprite.color = drugColor;
    }
}