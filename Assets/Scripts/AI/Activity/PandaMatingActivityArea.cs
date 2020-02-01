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
    public class PandaMatingActivityArea : MonoBehaviour
    {
        [SerializeField] private float _SexEroCost;
        [SerializeField] private float _SexTime;
        [SerializeField] private float _EroThresholdForSex;

        [SerializeField] private PandaActivityFlag _flag;

        private IPanda _panda;

        void Awake()
        {
            _panda = _flag.GetComponentNotNull<IPanda>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == gameObject)
            {
                return;
            }
            if (_panda.GetGender() == Gender.Male)
            {
                return;
            }
            if (other.gameObject.CompareTag(TagNames.Panda))
            {
                var otherPanda = other.gameObject.GetComponentNotNull<IPanda>();
                var otherPandaMatingActivity = other.gameObject.GetComponentInChildrenNotNull<PandaMatingActivityArea>();
                if (CanMateWith(otherPanda) && otherPandaMatingActivity.CanMateWith(_panda))
                {
                    StartMating(otherPandaMatingActivity);
                    otherPandaMatingActivity.StartMating(this);
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
            _panda.ChangeEro(_SexEroCost);
            Debug.Log("Fucky-fucky");
            yield return new WaitForSeconds(_SexTime); 
            _flag.RemoveActivity(otherPandaMatingActivityArea);
        }

        public bool CanMateWith(IPanda otherPanda)
        {
            return (!_flag.IsDuringActivity) && (otherPanda.GetGender() != _panda.GetGender()) && (_panda.GetEro() > _EroThresholdForSex);
        }
    }
}
