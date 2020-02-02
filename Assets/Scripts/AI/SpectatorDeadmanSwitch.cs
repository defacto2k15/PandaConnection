using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class SpectatorDeadmanSwitch : MonoBehaviour
    {
        public float MaximumLifetime;

        void Start()
        {
            StartCoroutine(DeadmanCoroutine());
        }

        private IEnumerator DeadmanCoroutine()
        {
            yield return new WaitForSeconds(MaximumLifetime);
            GameObject.Destroy(gameObject);
        }
    }
}
