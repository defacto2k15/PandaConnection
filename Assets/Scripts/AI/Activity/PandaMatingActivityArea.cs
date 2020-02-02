using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.PandaLogic.Genetics;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI.Activity
{
    public class PandaMatingActivityArea : MonoBehaviour
    {
        public ChildPandaCreator ChildPandaCreator;
        [SerializeField] private float _SexEroCost;
        [SerializeField] private float _SexTime;
        [SerializeField] private float _EroThresholdForSex;

        [SerializeField] private PandaActivityFlag _flag;

        private DummyPanda _thisPanda;
        private List<DummyPanda> _hotPandasInThisArea;

        void Awake()
        {
            _hotPandasInThisArea = new List<DummyPanda>();
            _thisPanda = _flag.GetComponentNotNull<DummyPanda>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == gameObject)
            {
                return;
            }
            if (_thisPanda.GetGender() == Gender.Male)
            {
                return;
            }
            if (other.gameObject.CompareTag(TagNames.Panda))
            {
                var otherPanda = other.gameObject.GetComponentNotNull<DummyPanda>();
                _hotPandasInThisArea.Add(otherPanda);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == gameObject)
            {
                return;
            }
            if (_thisPanda.GetGender() == Gender.Male)
            {
                return;
            }
            if (other.gameObject.CompareTag(TagNames.Panda))
            {
                var otherPanda = other.gameObject.GetComponentNotNull<DummyPanda>();
                Assert.IsTrue(_hotPandasInThisArea.Remove(otherPanda));
            }
        }


        void Update()
        {
            if (_hotPandasInThisArea.Any())
            {
                foreach (var otherPanda in _hotPandasInThisArea)
                {
                    var otherPandaMatingActivity = otherPanda.gameObject.GetComponentInChildrenNotNull<PandaMatingActivityArea>();
                    if (CanMateWith(otherPanda) && otherPandaMatingActivity.CanMateWith(_thisPanda))
                    {
                        StartMating(otherPandaMatingActivity);
                        otherPandaMatingActivity.StartMating(this);
                        return;
                    }
                }
            }
        }

        private void StartMating(PandaMatingActivityArea otherPandaMatingActivityArea)
        {
            StartCoroutine(MatingCoroutine(otherPandaMatingActivityArea));
        }

        private IEnumerator MatingCoroutine(PandaMatingActivityArea otherPandaMatingActivityArea)
        {
            _flag.AddActivity(otherPandaMatingActivityArea);
            _thisPanda.ChangeEro(_SexEroCost);
            _thisPanda.GetComponentNotNull<NavMeshAgent>().velocity = Vector3.zero;
            _thisPanda.GetComponentNotNull<NavMeshAgent>().isStopped = true;
            _thisPanda.StartAnimationState(PandaAnimationState.Sexing);
            Debug.Log("Fucky-fucky");
            yield return new WaitForSeconds(_SexTime); 
            _thisPanda.GetComponentNotNull<NavMeshAgent>().isStopped = false;
            _thisPanda.StopAnimationState(PandaAnimationState.Sexing);
            if (_thisPanda.GetGender() == Gender.Female)
            {
                Debug.Log("Created child");
                ChildPandaCreator.InstantianteChild(_thisPanda.gameObject, otherPandaMatingActivityArea._thisPanda.gameObject);
            }

            _flag.RemoveActivity(otherPandaMatingActivityArea);
        }

        public bool CanMateWith(IPanda otherPanda)
        {
            return (!_flag.IsDuringActivity) && (otherPanda.GetGender() != _thisPanda.GetGender()) && (_thisPanda.GetEro() > _EroThresholdForSex);
        }
    }
}
