using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI
{
    public class FoodPile : MonoBehaviour
    {
        [SerializeField] private List<FoodWithItsAmount> FoodConsumables;
        public BaseDrugConsumable Drug;

        public float MaxRange
        {
            get
            {

            if (! FoodConsumables.Any())
            {
                return 0;
            }
            else
            {
                var result = FoodConsumables.Max(c => c.Food.range);
                return result;
            }
            }
        }

        void Update()
        {
            FoodConsumables = FoodConsumables.Where(c => c.Amount > 0).ToList();
        }

        public void DestroyPile()
        {
            GameObject.Destroy(gameObject);
        }

        public ConsumableWithDrug RetriveFoodFromPile()
        {
            Assert.IsTrue(FoodConsumables.Any());
            var bestFood = FoodConsumables.OrderByDescending(c => c.Food.range).First();
            bestFood.Amount--;
            if (bestFood.Amount <= 0)
            {
                FoodConsumables = FoodConsumables.Where(c => c.Food != bestFood.Food).ToList();
            }

            return new ConsumableWithDrug()
            {
                Food = bestFood.Food,
                Drug = Drug
            };
        }

        public void ThereAreNoEatinPandasLeft()
        {
            if (!FoodConsumables.Any())
            {
                DestroyPile();
            }
        }

        public bool HasEadibleFoodForPanda(IPanda panda)
        {
            return FoodConsumables.Any(c => PandaCanConsumeFood(panda, c));
        }

        private static bool PandaCanConsumeFood(IPanda panda, FoodWithItsAmount c)
        {
            return ((IConsumable) c.Food).CanConsume(panda);
        }

        public float RetriveAttractionForPanda(IPanda panda)
        {

            return FoodConsumables.Sum(c =>
            {
                if (!PandaCanConsumeFood(panda, c))
                {
                    return 0;
                }
                return c.Food.range * c.Amount;
            }) * 0.1f * Mathf.Max(0, 10 - panda.GetFullness());
        }

        public void PlaceFood(BaseFoodConsumable food)
        {
            var spawnedFood = GameObject.Instantiate<BaseFoodConsumable>(food, this.transform);
            FoodConsumables.Add(new FoodWithItsAmount() { Amount = (int)food.timeGivingNutrition, Food = spawnedFood });
        }

        public void PlaceDrug(BaseDrugConsumable drug)
        {
            Drug = drug;
        }
    }

    public class ConsumableWithDrug
    {
        public BaseFoodConsumable Food;
        public BaseDrugConsumable Drug;
    }
}