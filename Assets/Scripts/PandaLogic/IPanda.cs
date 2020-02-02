using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPanda
{
    PandaStats GetStats();

    void SetStats(PandaStats childStats);

    float GetHealth();

    void ChangeHealth(float deltaHealth);

    float GetEro();

    void ChangeEro(float deltaEro);

    float GetFullness();

    void ChangeFullness(float deltaFood);

    void GetBodyPartSize(BodyPart part);
    bool IsNotFull();
    Gender GetGender();

    void Select(int i);
    void Deselect(int i);
}