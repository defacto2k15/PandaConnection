using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResearchManager 
{
    List<IResearchItem> StartingItems { get; }
}

public interface IResearchItem
{
    List<IResearchItem> Requirements { get; }
    bool Bought { get; }
    bool IsBuyable();
    void Buy();
    int Cost { get; }
}
