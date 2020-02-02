using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Sounds;
using UnityEngine;

public class InventoryConsumableItemUI : BaseConsumableItemUI
{
    [SerializeField]
    protected FoodPlaceSeeker foodPlaceSeeker;

    [SerializeField]
    protected TMPro.TMP_Text m_count;

    public int count;

    public override void Init(IConsumable consumable)
    {
        base.Init(consumable);
        count = 1;
        m_count.text = count + "";
    }

    public void RefreshUI()
    {
        m_count.text = count + "";
    }

    public override void DoAction()
    {
        SoundManager.instance.PlayOneShotSound(SoundType.MenuClick);
        //TODO: spawnuje 3d klocek do ustawienia w swiecie. potem ten klocek lazi za myszka,
        //daje uzytkownikowi jakies info czy w tym miejscu mozna go stawiac, jak sie go postawi to dopiero on wywoluje konsumpcje i zdjecie itemu.
        //na razie obchodze ten proces, i po prostu wywoluje konsumpcje
        var fps = GameObject.Instantiate<FoodPlaceSeeker>(foodPlaceSeeker);
        fps.Consumable = Consumable;
    }

    public override void StartConsumeAnimation()
    {
        count--;
        RefreshUI();
        if (count == 0)
        {
            Destroy(this.gameObject);
        }
    }
}