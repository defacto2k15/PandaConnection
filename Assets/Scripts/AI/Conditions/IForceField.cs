
using UnityEngine;

namespace Assets.Scripts.AI.Conditions
{
    public interface IForceField
    {
        float RetriveAttraction(IPanda panda);
        float Radius { get;  }
        Vector3 WorldSpacePosition { get; }
    }
}