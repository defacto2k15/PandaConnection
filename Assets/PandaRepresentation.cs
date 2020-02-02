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
    
    public void LoadPanda(IPanda panda)
    {
        pandaName.text = panda.GetStats().name;
        gender.text = panda.GetStats().gender.ToString();
        primaryColor.SetText(panda.GetStats().Phenotype.primaryBodyColorTrait.ToString());
        secendaryColor.SetText(panda.GetStats().Phenotype.secondaryColorTrait.ToString());
        eyeColor.SetText(panda.GetStats().Phenotype.eyeColorTrait.ToString());
        eyeType.SetText(panda.GetStats().Phenotype.eyesTypeTrait.ToString());
        earType.SetText(panda.GetStats().Phenotype.earTypeTrait.ToString());
        mouthNoseType.SetText(panda.GetStats().Phenotype.noseMouthTypeTrait.ToString());
        tailType.SetText(panda.GetStats().Phenotype.tailTypeTrait.ToString());
        bodyType.SetText(panda.GetStats().Phenotype.bodyTypeTrait.ToString());
        specialType.SetText(panda.GetStats().Phenotype.specialTrait.ToString());
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
