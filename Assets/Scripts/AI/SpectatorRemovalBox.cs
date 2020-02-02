using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class SpectatorRemovalBox : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagNames.Spectator))
            {
                GameObject.Destroy(other.gameObject);
            }
        }
    }
}
