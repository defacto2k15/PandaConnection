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

        void Start()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagNames.Panda))
            {
                var panda = other.gameObject.GetComponentNotNull<IPanda>();
                var pandaActivityFlag = other.gameObject.GetComponentNotNull<PandaActivityFlag>();
                if (CanApplyPile(panda) && !_eatingPandas.Contains(panda) && PandaIsHungry(panda))
                {
                    StartCoroutine(Eating(panda, pandaActivityFlag));
                }
            }
        }

        private bool PandaIsHungry(IPanda panda)
        {
            return panda.IsNotFull();
        }

        private IEnumerator Eating(IPanda panda, PandaActivityFlag activityFlag)
        {
            Assert.IsFalse(_eatingPandas.Contains(panda));
            _eatingPandas.Add(panda);
            activityFlag.AddActivity(this);
            while (CanApplyPile(panda) && PandaIsHungry(panda))
            {
                var foodToEat = Pile.RetriveFoodFromPile();

                this.GetComponentNotNull<DummyPanda>().StartAnimationState(PandaAnimationState.Eating);
                Debug.Log("EATING: " + foodToEat.Food);
                yield return new WaitForSeconds(foodToEat.Food.timeGivingNutrition);
                Debug.Log("EATEN: " +  foodToEat.Food);
                this.GetComponentNotNull<DummyPanda>().StopAnimationState(PandaAnimationState.Eating);
                ((IConsumable) foodToEat.Food).Consume(panda);
                if (foodToEat.Drug != null)
                {
                    if (((IConsumable) foodToEat.Drug).CanConsume(panda))
                    {
                        Debug.Log("Consuming drug: "+foodToEat.Drug);
                        ((IConsumable)foodToEat.Drug).Consume(panda);
                    }
                }
            }

            activityFlag.RemoveActivity(this);
            _eatingPandas.Remove(panda);
            Pile.ThereAreNoEatinPandasLeft();
        }

        private bool CanApplyPile(IPanda panda)
        {
            return Pile.HasEadibleFoodForPanda(panda);
        }
    }
}
