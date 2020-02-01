using System;
using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Tuple<T1, T2>
{
    public T1 t1;
    public T2 t2;
}

public class Util
{
    public static Vector3 CalculateRaycastPosition()
    {
        Ray ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, GameManager.instance.floorMask))
        {
            var selectedPosition = hit.point;
            return selectedPosition;
        }

        return Vector3.one * int.MinValue;
    }

    public static FoodPile RaycastFoodPile()
    {
        Ray ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, GameManager.instance.foodPileMask))
        {
            var selectedFoodPile = hit.collider.GetComponentInParent<FoodPile>();
            return selectedFoodPile;
        }

        return null;
    }

    public static IPanda RaycastPanda()
    {
        Ray ray = GameManager.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, GameManager.instance.pandaMask))
        {
            var selectedPanda = hit.collider.GetComponentInParent<IPanda>();
            return selectedPanda;
        }

        return null;
    }
}
