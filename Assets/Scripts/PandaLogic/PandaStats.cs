using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PandaStats
{
    public string name;
    public int birthdate;
    public List<Tuple<IChromosome, IChromosome>> chromosomes; 
}

public enum BodyPart
{
    leftLeg,
    rightLeg,
    leftArm,
    rightArm,
    ears,
    head,
    eyes,
    muzzle
}


