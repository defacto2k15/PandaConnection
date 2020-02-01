using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.AI.Conditions
{
    [RequireComponent(typeof(SphereCollider))]
    public class SimpleForceField : MonoBehaviour, IForceField
    {
        [Range(-5,5)]
        [SerializeField] private float ForceMagnitude;

        private SphereCollider _sphereCollider;

        void Awake()
        {
            _sphereCollider = this.GetComponentNotNull<SphereCollider>();
        }

        public float RetriveAttraction(IPanda panda)
        {
            return ForceMagnitude;
        }

        public float Radius => _sphereCollider.radius;
        public Vector3 WorldSpacePosition => transform.position;
    }
}