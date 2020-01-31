using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaStats
{
    string name;
    int birthdate;
    List<Tuple<IChromosome, IChromosome>> chromosomes; 
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


