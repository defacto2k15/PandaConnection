using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PandaRepresentation : MonoBehaviour
{
    public TMPro.TMP_Text pandaName;
    public TMPro.TMP_Text gender;
    public TMPro.TMP_Text primaryColor;
    public TMPro.TMP_Text secendaryColor;
    public TMPro.TMP_Text eyeColor;
    public TMPro.TMP_Text eyeType;
    public TMPro.TMP_Text earType;
    public TMPro.TMP_Text mouthNoseType;
    public TMPro.TMP_Text tailType;
    public TMPro.TMP_Text bodyType;
    public TMPro.TMP_Text specialType;
    
    public void LoadPanda(PandaStats stats)
    {
        pandaName.text = stats.name;
        gender.text = stats.gender.ToString();
        primaryColor.SetText(stats.Phenotype.primaryBodyColorTrait.ToString());
        secendaryColor.SetText(stats.Phenotype.secondaryColorTrait.ToString());
        eyeColor.SetText(stats.Phenotype.eyeColorTrait.ToString());
        eyeType.SetText(stats.Phenotype.eyesTypeTrait.ToString());
        earType.SetText(stats.Phenotype.earTypeTrait.ToString());
        mouthNoseType.SetText(stats.Phenotype.noseMouthTypeTrait.ToString());
        tailType.SetText(stats.Phenotype.tailTypeTrait.ToString());
        bodyType.SetText(stats.Phenotype.bodyTypeTrait.ToString());
        specialType.SetText(stats.Phenotype.specialTrait.ToString());
    }

    internal void Clear()
    {
        pandaName.text = "";
        gender.text = "";
        primaryColor.SetText("");
        secendaryColor.SetText("");
        eyeColor.SetText("");
        eyeType.SetText("");
        earType.SetText("");
        mouthNoseType.SetText("");
        tailType.SetText("");
        bodyType.SetText("");
        specialType.SetText("");

    }
}
