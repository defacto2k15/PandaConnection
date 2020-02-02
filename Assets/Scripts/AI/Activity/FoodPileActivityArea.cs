using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Sounds;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI
{
    public class FoodPileActivityArea : MonoBehaviour
    {
        [SerializeField] private FoodPile Pile;

        private List<IPanda> _eatingPandas;

        private void Awake()
        {
            _eatingPandas = new List<IPanda>();
        }

        private void Start()
        {
        }

        private void OnTriggerEnter(Collider other)
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

            var dummyPanda = (panda as DummyPanda);
            var navmeshAgent = dummyPanda.GetComponentNotNull<NavMeshAgent>();
            while (CanApplyPile(panda) && PandaIsHungry(panda))
            {
                navmeshAgent.velocity = Vector3.zero;
                navmeshAgent.isStopped = true;
                dummyPanda.GetComponentNotNull<PandaSpriteOrientationChanger>()
                    .AlignOrientationToVelocity((transform.position - dummyPanda.transform.position).normalized);
                //UpdateNavmeshAgent(panda);
                var foodToEat = Pile.RetriveFoodFromPile();

                dummyPanda.StartAnimationState(PandaAnimationState.Eating);
                Debug.Log("EATING: " + foodToEat.Food);

                var foodTimeGivingNutrition = foodToEat.Food.timeGivingNutrition;
                SoundManager.instance.PlaySustainedTheme(dummyPanda.gameObject, SoundType.Eating, foodTimeGivingNutrition+0.3f );
                yield return new WaitForSeconds(foodTimeGivingNutrition);
                Debug.Log("EATEN: " + foodToEat.Food);
                if (panda == null)
                {
                    break;
                }
                dummyPanda.StopAnimationState(PandaAnimationState.Eating);
                ((IConsumable)foodToEat.Food).Consume(panda);
                if (foodToEat.Drug != null)
                {
                    if (((IConsumable)foodToEat.Drug).CanConsume(panda))
                    {
                        Debug.Log("Consuming drug: " + foodToEat.Drug);
                        (foodToEat.Drug).DoAction(panda);
                    }
                }
                navmeshAgent.isStopped =false;
            }

            if (!(panda == null))
            {
                activityFlag.RemoveActivity(this);
            }

            _eatingPandas.Remove(panda);
            if (!_eatingPandas.Any())
            {
                Pile.ThereAreNoEatinPandasLeft();
            }
        }

        private bool CanApplyPile(IPanda panda)
        {
            return Pile.HasEadibleFoodForPanda(panda);
        }
    }
}