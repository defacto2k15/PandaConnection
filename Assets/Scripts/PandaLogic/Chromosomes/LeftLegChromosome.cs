using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zwraca 0 jesli nogi sa duze, 1 jesli male, 2 jesli bez nog
/// </summary>
public class LeftLegChromosome : MonoBehaviour, IChromosome
{
    public enum LegType
    {
        big = 0,
        small = 1,
        noLegs = 2
    }
    public LegType myLegType;
    int IChromosome.GetValue(IChromosome other)
    {
        var otherChromosome = other as LeftLegChromosome;
        return ((int)myLegType + (int)otherChromosome.myLegType) % 3;
    }
}
