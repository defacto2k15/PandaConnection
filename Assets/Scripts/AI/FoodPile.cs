using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FoodPile : MonoBehaviour
    {
        private MeshRenderer _renderer;
        public List<FoodWithItsAmount> FoodConsumables;

        void Awake()
        {
            _renderer = this.GetComponentNotNull<MeshRenderer>();
        }

        void Update()
        {
            FoodConsumables = FoodConsumables.Where(c => c.Amount > 0).ToList();
        }

        public void DestroyPile()
        {
            GameObject.Destroy(gameObject);
        }

        public void RemoveVisualObject()
        {
            _renderer.enabled = false;
        }
    }
}