using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.AI.Activity
{
    [RequireComponent(typeof(DummyPanda))]
    public class PandaFullnessActivity : MonoBehaviour
    {
        [SerializeField] private float TimeBetweenFullnessChanges;
        [SerializeField] private float FullnessChangeDelta;
        [SerializeField] private float DyingTime;
        private DummyPanda _panda;
        private PandaActivityFlag _flag;

        void Awake()
        {
            _panda = this.GetComponentNotNull<DummyPanda>();
            _flag = this.GetComponentNotNull<PandaActivityFlag>();
        }

        void Start()
        {
            StartCoroutine(FullnessChanging());
        }

        private IEnumerator FullnessChanging()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeBetweenFullnessChanges);
                if (_panda == null)
                {
                    break;
                }
                _panda.ChangeFullness(FullnessChangeDelta);

                if (_panda.GetFullness() <= 0)
                {
                    StartCoroutine(Die());
                    break;
                }
            }
        }

        private IEnumerator Die()
        {
            _flag.AddActivity(this);
            _panda.StartAnimationState(PandaAnimationState.Dying);
            Debug.Log("Staring death of panda becouse of hunger");
            yield return new WaitForSeconds(DyingTime);
            Debug.Log("Killing panda becouse of hunger");
            GameObject.Destroy(gameObject);
        }
    }
}
