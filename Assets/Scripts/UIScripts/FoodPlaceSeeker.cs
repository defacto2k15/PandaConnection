using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlaceSeeker : MonoBehaviour
{
    public IConsumable Consumable;
    public MeshRenderer meshIndicator;
    public Transform rangeIndicator;

    private bool done = false;

    private static FoodPlaceSeeker instance;

    private void Start()
    {
        //magiczny float ze skalowania range dla food pile
        var range = Consumable.GetRange() * 2.3046f;
        rangeIndicator.transform.localScale = Vector3.one * Consumable.GetRange();// * 2.3046f;

        //retard singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            GameObject.Destroy(instance.gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            return;
        }
        var newPosition = Util.CalculateRaycastPosition();
        //Debug.Log(newPosition);
        this.transform.position = new Vector3(newPosition.x, this.transform.position.y, newPosition.z);
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
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
