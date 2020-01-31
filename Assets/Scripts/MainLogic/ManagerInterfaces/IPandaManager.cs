using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//trzyma stado pand, zarzadza ich update, zmienia im stany, usuwa je do time outu, bo n.p. sa chore lub ogladaja porno
public interface IPandaManager
{
     PandaStats ComputePairingStats(IPanda panda1, IPanda panda2);
    void BoostHealth(int healthBoost);
    void Select(IPanda panda, int slot);
     void Unselect(int slot);
     IPanda GetFirstSelectedPanda();
     IPanda GetSecondSelectedPanda();
}
