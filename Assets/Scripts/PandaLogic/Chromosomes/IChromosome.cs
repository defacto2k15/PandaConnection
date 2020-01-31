using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChromosome
{
    int GetValue(IChromosome other);
}
