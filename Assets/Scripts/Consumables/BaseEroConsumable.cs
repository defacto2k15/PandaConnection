using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEroConsumable : BaseConsumable
{
    [SerializeField]
    private int range;

    public Sprite mySprite;

    [SerializeField]
    private int m_eroNutritionalValue;

    [SerializeField]
    private int timeGivingNutrition;

    public override void DoAction(IPanda panda)
    {
        panda.ChangeEro(m_eroNutritionalValue);
    }

    public int Range => range;

    public int MEroNutritionalValue => m_eroNutritionalValue;

    public int TimeGivingNutrition => timeGivingNutrition;

    public override bool CanPlace()
    {
        return Util.RaycastFoodPile() == null && Util.CalculateRaycastPosition().z > -2000;
    }

    public override void PlaceInWorld()
    {
        var place = Util.CalculateRaycastPosition();
        var eroPile = GameObject.Instantiate<EroPileActivityArea>(GameManager.instance.eroPilePrefab);
        eroPile.transform.position = place;
        eroPile.SetEroConsumable(this);
        eroPile.foodSprite.sprite = mySprite;
    }

    public override float GetRange()
    {
        return range;
    }
}