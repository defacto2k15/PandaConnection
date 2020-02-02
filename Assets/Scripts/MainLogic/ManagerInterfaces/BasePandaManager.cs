using System;
using System.Collections.Generic;
using UnityEngine;

public class BasePandaManager : MonoBehaviour, IPandaManager
{
    [SerializeField] private float MaximumFullness;
    [SerializeField] private float MaximumEro;
    [SerializeField] private float MaximumHealth;
    [SerializeField] private float MinimumFullnessForMating;

    public PandaStats ComputePairingStats(IPanda panda1, IPanda panda2)
    {
        throw new System.NotImplementedException();
    }

    public void BoostHealth(int healthBoost)
    {
        throw new System.NotImplementedException();
    }

    public void Select(IPanda panda, int slot)
    {
        throw new System.NotImplementedException();
    }

    internal void ChangeCashMultiplier(int value)
    {
        GameManager.instance.crowdManager.cashMultiplayer += value;
    }

    public void Unselect(int slot)
    {
        throw new System.NotImplementedException();
    }

    public IPanda GetFirstSelectedPanda()
    {
        throw new System.NotImplementedException();
    }

    internal void ChangeInfectionChange(int value)
    {
        throw new NotImplementedException();
    }

    public IPanda GetSecondSelectedPanda()
    {
        throw new System.NotImplementedException();
    }

    internal void BoostMaxFood(int value)
    {
        MaximumFullness += value;
    }

    internal void BoostMaxEro(int value)
    {
        MaximumEro += value;
    }

    public float GetMaximumFullness()
    {
        return MaximumFullness;
    }

    public float GetMaximumEro()
    {
        return MaximumEro;
    }

    public float GetMaximumHealth()
    {
        return MaximumHealth;
    }

    public float GetMinimumFullnessForMating()
    {
        return MinimumFullnessForMating;
    }

    [HideInInspector]
    public List<IPanda> pandasOnDisplay = new List<IPanda>();

    public int timeGenesMutated = 0;

    public int pandasReleasedToForrest = 0;

    public int pandasToWin = 25;
}