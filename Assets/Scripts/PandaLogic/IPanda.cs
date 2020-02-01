﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPanda
{
    PandaStats GetStats();
    float GetHealth();
    void SetHealth(float deltaHealth);
    float GetFood();
    void SetFood(float deltaFood);
    float GetErotic();
    void SetErotic(float deltaErotic);
    void GetPrimaryColor();
    void GetSecondaryColor();
    void GetBodyPartSize(BodyPart part);
}
