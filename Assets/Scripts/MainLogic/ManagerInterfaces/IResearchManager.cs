using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResearchManager 
{
    List<IResearchItem> startingItems { get; }
}

public interface IResearchItem
{
    List<IResearchItem> children { get; }
    List<IResearchItem> requirements { get; }
    bool bought { get; }
    bool IsBuyable();
    void Buy();
    int cost { get; }
}
