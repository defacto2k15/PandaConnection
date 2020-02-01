using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PandaLogic.Genetics;
using UnityEngine;

[Serializable]
public class PandaStats
{
    public string name;
    public int birthdate;
    public List<Tuple<IChromosome, IChromosome>> chromosomes;
    public Gender gender;
    public Phenotype Phenotype;
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

public enum Gender
{
    Male, Female
}

[Serializable]
public class Phenotype
{
    public LegLengthTrait LegLengthTrait;
    public EyeColorTrait EyeColorTrait;
    public TailTypeTrait TailTypeTrait;
}

