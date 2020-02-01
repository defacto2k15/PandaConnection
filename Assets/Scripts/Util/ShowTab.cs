using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowTab : MonoBehaviour
{
    public Transform tabToShow;

    private void Awake()
    {
        var button = this.GetComponent<Button>();
        button.onClick.AddListener(ShowTabClicked);
    }
    private void ShowTabClicked()
    {
        tabToShow.SetSiblingIndex(tabToShow.parent.childCount - 1);
    }
}
