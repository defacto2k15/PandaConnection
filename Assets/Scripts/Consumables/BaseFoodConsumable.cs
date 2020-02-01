﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.AI;

public class BaseFoodConsumable : BaseConsumable
{
    [SerializeField]
    public float range;

    [SerializeField]
    private float m_foodNutritionalValue;

    [SerializeField]
    public float timeGivingNutrition;

    protected override void DoAction(IPanda panda)
    {
        panda.ChangeFullness(m_foodNutritionalValue);
    }

    public override bool CanPlace()
    {
        return Util.RaycastFoodPile()==null && Util.RaycastEroPile()==null;
    }

    public override void PlaceInWorld()
    {
        var place = Util.CalculateRaycastPosition();
        var foodPile = GameObject.Instantiate<FoodPile>(GameManager.instance.foodPilePrefab);
        foodPile.transform.position = place;
        foodPile.PlaceFood(this);
    }
}