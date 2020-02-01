using UnityEngine;

public class BasePandaManager : MonoBehaviour, IPandaManager
{
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

    public void Unselect(int slot)
    {
        throw new System.NotImplementedException();
    }

    public IPanda GetFirstSelectedPanda()
    {
        throw new System.NotImplementedException();
    }

    public IPanda GetSecondSelectedPanda()
    {
        throw new System.NotImplementedException();
    }

    public float GetMaximumFullness()
    {
        return 100;
    }
}