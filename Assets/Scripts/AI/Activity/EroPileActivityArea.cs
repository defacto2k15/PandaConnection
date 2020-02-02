using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class EroPileActivityArea : MonoBehaviour
    {
        [SerializeField] private BaseEroConsumable _eroConsumable;
        [SerializeField] private float _tickDurationInSeconds;
        public SpriteRenderer foodSprite;

        [SerializeField]
        private Transform _rangeSignalizer;

        private CapsuleCollider _sphereCollider;
        private MeshRenderer _meshRenderer;
        private List<IPanda> _usingPandas;
        private bool _isActive;

        private void Awake()
        {
            if (_eroConsumable != null)
            {
                Init();
            }
        }

        public void SetEroConsumable(BaseEroConsumable consumable)
        {
            _eroConsumable = consumable;
            Init();
        }

        private void Init()
        {
            _sphereCollider = this.GetComponentNotNull<CapsuleCollider>();
            _rangeSignalizer.localScale = Vector3.one * _eroConsumable.Range * this.transform.localScale.x * 2.2f;
            _sphereCollider.radius = _eroConsumable.Range;
            _meshRenderer = this.GetComponentNotNull<MeshRenderer>();
            _usingPandas = new List<IPanda>();
            _isActive = true;
            StartCoroutine(TurnOffEro());
        }

        private IEnumerator TurnOffEro()
        {
            yield return new WaitForSeconds(_eroConsumable.TimeGivingNutrition);
            _isActive = false;
            _meshRenderer.enabled = false;
            TryDestroyingActivityArea();
        }

        private void TryDestroyingActivityArea()
        {
            GameObject.Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagNames.Panda))
            {
                if (_isActive)
                {
                    var panda = other.gameObject.GetComponentNotNull<IPanda>();
                    Assert.IsFalse(_usingPandas.Contains(panda));
                    _usingPandas.Add(panda);
                    StartCoroutine(Using(panda));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagNames.Panda))
            {
                var panda = other.gameObject.GetComponentNotNull<IPanda>();
                if (_usingPandas != null && _usingPandas.Contains(panda))
                {
                    _usingPandas.Remove(panda);
                    TryDestroyingActivityArea();
                }
            }
        }

        private IEnumerator Using(IPanda panda)
        {
            float timeStarted = Time.time;
            while (_isActive && _usingPandas.Contains(panda) && Time.time - timeStarted < _eroConsumable.TimeGivingNutrition)
            {
                if (((IConsumable)_eroConsumable).CanConsume(panda))
                {
                    Debug.Log("Using ERO");
                    _eroConsumable.DoAction(panda);
                    yield return new WaitForSeconds(_tickDurationInSeconds);
                }
            }

            if (_usingPandas.Contains(panda))
            {
                _usingPandas.Remove(panda);
            }

            TryDestroyingActivityArea();
        }
    }
}