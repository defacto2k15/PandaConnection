using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI.Conditions;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class FoodPileForceField : MonoBehaviour, IForceField
    {
        [SerializeField] private FoodPile _pile;
        [SerializeField] private Transform _radiusSignalizer;

        private CapsuleCollider _collider;

        void Awake()
        {
            _collider = this.GetComponentNotNull<CapsuleCollider>();
            UpdateRadius();
        }

        private void UpdateRadius()
        {
            var range = _pile.MaxRange;
            _radiusSignalizer.localScale = Vector3.one * range;
            _collider.radius = range;
        }

        public float RetriveAttraction(IPanda panda)
        {
            return _pile.RetriveAttractionForPanda(panda);
        }

        void Update()
        {
            UpdateRadius();
        }

        public float Radius => _collider.radius;
        public Vector3 WorldSpacePosition {
            get
            {
                if (this.transform != null)
                {
                    return this.transform.position;
                }
                return Vector3.zero * int.MinValue;
            }
        }
    }

    [Serializable]
    public class FoodWithItsAmount
    {
        public BaseFoodConsumable Food;
        public int Amount;
    }
}
