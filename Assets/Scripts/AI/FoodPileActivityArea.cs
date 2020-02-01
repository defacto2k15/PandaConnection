using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI
{
    public class FoodPileActivityArea : MonoBehaviour
    {
        [SerializeField] private FoodPile Pile;

        private List<IPanda> _eatingPandas;

        void Awake()
        {
            _eatingPandas = new List<IPanda>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagNames.Panda))
            {
                var panda = other.gameObject.GetComponentNotNull<IPanda>();
                var pandaActivityFlag = other.gameObject.GetComponentNotNull<PandaActivityFlag>();
                if (CanApplyPile() && !_eatingPandas.Contains(panda))
                {
                    StartCoroutine(Eating(panda, pandaActivityFlag));
                }
            }
        }

        private IEnumerator Eating(IPanda panda, PandaActivityFlag activityFlag)
        {
            Assert.IsFalse(_eatingPandas.Contains(panda));
            _eatingPandas.Add(panda);
            activityFlag.AddActivity(this);
            while (CanApplyPile())
            {
                var mostAttractiveFood = Pile.FoodConsumables.OrderByDescending(c => c.Food.range).First();
                mostAttractiveFood.Amount--;
                if (mostAttractiveFood.Amount <= 0)
                {
                    Pile.FoodConsumables.Remove(mostAttractiveFood);
                }

                Debug.Log("EATING: " + mostAttractiveFood.Food);
                yield return new WaitForSeconds(mostAttractiveFood.Food.timeGivingNutrition);
                Debug.Log("EATEN: " + mostAttractiveFood.Food);
                ((IConsumable) mostAttractiveFood.Food).Consume(panda);
            }

            activityFlag.RemoveActivity(this);
            _eatingPandas.Remove(panda);
            if (!Pile.FoodConsumables.Any() && !_eatingPandas.Any())
            {
                Pile.DestroyPile();
            }
        }

        private bool CanApplyPile()
        {
            return Pile.FoodConsumables.Any(c => c.Amount > 0);
        }
    }
}
