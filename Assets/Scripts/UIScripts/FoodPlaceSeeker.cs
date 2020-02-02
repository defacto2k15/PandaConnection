﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlaceSeeker : MonoBehaviour
{
    public IConsumable Consumable;
    public MeshRenderer meshIndicator;
    public Transform rangeIndicator;

    private bool done = false;

    private void Start()
    {
        //magiczny float ze skalowania range dla food pile
        var range = Consumable.GetRange() * 2.3046f;
        Debug.LogError(range);
        rangeIndicator.transform.localScale = Vector3.one*Consumable.GetRange()* 2.3046f;
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            return;
        }
        this.transform.position = Util.CalculateRaycastPosition();
        bool canPlace = (Consumable?.CanPlace()).GetValueOrDefault();
        if (canPlace)
        {
            meshIndicator.material.color = Color.green;
        }
        else
        {
            meshIndicator.material.color = Color.red;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canPlace)
            {
                GameManager.instance.ConsumableManager.Consume(Consumable);
                GameObject.Destroy(this.gameObject);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}