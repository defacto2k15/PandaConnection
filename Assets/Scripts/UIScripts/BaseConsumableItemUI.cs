using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseConsumableItemUI : MonoBehaviour
{
    [SerializeField]
    protected Button m_button;

    [SerializeField]
    protected Image m_sprite;

    [SerializeField]
    protected TMPro.TMP_Text m_name;

    public IConsumable Consumable { get; private set; }

    public virtual void Init(IConsumable consumable)
    {
        this.Consumable = consumable;
        m_name.SetText(consumable.GetName());
        m_sprite.sprite = consumable.GetIcon();
        m_button.onClick.AddListener(DoAction);
    }

    public virtual void DoAction()
    {
    }

    public virtual void StartConsumeAnimation()
    {
        Destroy(this.gameObject);
    }
}