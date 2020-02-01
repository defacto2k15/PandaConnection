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

        private CapsuleCollider _collider;

        void Awake()
        {
            _collider = this.GetComponentNotNull<CapsuleCollider>();
            UpdateRadius();
        }

        private void UpdateRadius()
        {
            if (!_pile.FoodConsumables.Any())
            {
                _collider.radius = 0;
            }
            else
            {
                _collider.radius = _pile.FoodConsumables.Max(c => c.Food.range);
            }
        }

        public float RetriveAttraction(IPanda panda)
        {
            var attractionFactor = _pile.FoodConsumables.Sum(c => c.Food.range * c.Amount) * 0.1f * (1 - panda.GetFullness());
            return attractionFactor;
        }

        void Update()
        {
            UpdateRadius();
        }

        public float Radius => _collider.radius;
        public Vector3 WorldSpacePosition => transform.position;
    }

    [Serializable]
    public class FoodWithItsAmount
    {
        public BaseFoodConsumable Food;
        public int Amount;
    }
}
