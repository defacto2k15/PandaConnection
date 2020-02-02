using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PandaSpriteOrientationChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToChangeOrientation;
        [SerializeField] private int _verticalAngleChange;
        private NavMeshAgent _agent;

        private float _initialZScale;

        void Awake()
        {
            _agent = this.GetComponentNotNull<NavMeshAgent>();
            _initialZScale = _objectToChangeOrientation.transform.localScale.z;
        }

        void Update()
        {
            var horizontalVelocity = _agent.velocity.z;
            float horizontalMultiplier = Mathf.Sign(horizontalVelocity);
            if (Mathf.Abs(horizontalVelocity) > 0.1)
            {
                var oldLocalScale = _objectToChangeOrientation.transform.localScale;
                _objectToChangeOrientation.transform.localScale = new Vector3(oldLocalScale.x, oldLocalScale.y, _initialZScale * horizontalMultiplier);
            }

            var localZScaleSign = Mathf.Sign(_objectToChangeOrientation.transform.localScale.z);
            var verticalVelocity = _agent.velocity.x;
            if (Mathf.Abs(verticalVelocity ) > 0.1)
            {
                float verticalMultiplier = Mathf.Sign(verticalVelocity);
                var oldEulerRotation = _objectToChangeOrientation.transform.rotation.eulerAngles;

                if (verticalMultiplier < 0)
                {
                    _objectToChangeOrientation.transform.rotation = Quaternion.Euler(_verticalAngleChange * -1 * localZScaleSign, oldEulerRotation.y, oldEulerRotation.z);
                }
                else
                {
                    _objectToChangeOrientation.transform.rotation = Quaternion.Euler(0, oldEulerRotation.y, oldEulerRotation.z);
                }

            }

        }

    }
}
