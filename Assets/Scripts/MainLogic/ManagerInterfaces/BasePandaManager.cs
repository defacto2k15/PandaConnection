using System;
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    internal void BoostMaxEro(int value)
    {
        throw new NotImplementedException();
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

}