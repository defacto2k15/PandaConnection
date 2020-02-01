using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryConsumableItemUI : BaseConsumableItemUI
{
    public override void DoAction()
    {
        //TODO: spawnuje 3d klocek do ustawienia w swiecie. potem ten klocek lazi za myszka, 
        //daje uzytkownikowi jakies info czy w tym miejscu mozna go stawiac, jak sie go postawi to dopiero on wywoluje IConsumableManager.
        //na razie obchodze ten proces, i po prostu wywoluje konsumpcje
        GameManager.instance.ConsumableManager.Consume(Consumable);
    }
}
