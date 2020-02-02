using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowTab : MonoBehaviour
{
    public bool startHighlighted;
    public Transform tabToShow;
    private ShowTab[] otherTabs;
    public Color highlightedColor;
    public Color unhighlightedColor;

    private void Awake()
    {
        var button = this.GetComponent<Button>();
        button.onClick.AddListener(ShowTabClicked);
        otherTabs = this.transform.parent.GetComponentsInChildren<ShowTab>();
        if (startHighlighted)
        {
            ShowTabClicked();
        }
    }
    private void ShowTabClicked()
    {
        tabToShow.SetSiblingIndex(tabToShow.parent.childCount - 1);
        Highlight();
        foreach(var tab in otherTabs)
        {
            if (tab != this)
            {
                tab.UnHighlight();
            }
        }
    }

    public void UnHighlight()
    {
        this.GetComponent<Image>().color = unhighlightedColor;
    }

    public void Highlight()
    {
        this.GetComponent<Image>().color = highlightedColor;
    }
}
