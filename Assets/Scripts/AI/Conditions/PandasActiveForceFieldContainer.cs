﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI.Conditions
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(IPanda))]
    public class PandasActiveForceFieldContainer : MonoBehaviour
    {
        private List<IForceField> _activeForceFields;
        private IPanda _panda;

        void Awake()
        {
            _activeForceFields = new List<IForceField>();
            _panda = this.GetComponentNotNull<IPanda>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagNames.ForceField))
            {
                _activeForceFields.Add( other.gameObject.GetComponentNotNull<IForceField>());
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagNames.ForceField))
            {
                Assert.IsTrue(_activeForceFields.Remove( other.gameObject.GetComponentNotNull<IForceField>()));
            }
        }

        public bool IsInActiveFieldArea => _activeForceFields.Any() &&
                                           _activeForceFields.Select(c =>
                                               Mathf.Abs(AttractionModifiedByDistanceFactor(c, (c.WorldSpacePosition - transform.position).magnitude))).Sum() > 0.1f;

        private float AttractionModifiedByDistanceFactor(IForceField c, float distance)
        {

            var distanceFactor = Mathf.InverseLerp(c.Radius + 3, 0, distance);
            return (distanceFactor * c.RetriveAttraction(_panda));
        }

        public Vector3 ForceVector
        {
            get
            {
                Assert.IsTrue(_activeForceFields.Any());
                return (_activeForceFields.Select(c =>
                {
                    var positionDelta = c.WorldSpacePosition - transform.position;
                    return AttractionModifiedByDistanceFactor(c, positionDelta.magnitude) * positionDelta;
                }).Aggregate(Vector3.zero, (a, b) => a + b) / _activeForceFields.Count).normalized;
            }
        }
    }
}