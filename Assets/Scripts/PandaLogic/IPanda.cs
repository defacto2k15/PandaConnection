using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPanda
{
    PandaStats GetStats();

    float GetHealth();

    void SetHealth(float deltaHealth);

    float GetEro();

    void SetEro(float deltaEro);

    float GetFullness();

    void ChangeFullness(float deltaFood);

    void GetPrimaryColor();

    void GetSecondaryColor();

    void GetBodyPartSize(BodyPart part);
}