using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Sounds;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.AI
{
    public class SpectatorCameraFlashing : MonoBehaviour
    {
        public Vector2 TimeBetweenFlashesRange;
        public bool FlashingEnabled;
        public ParticleSystem ParticleSystemPrefab;
        public GameObject ParticleSpawnPosition;

        void Start()
        {
            StartCoroutine(Flashing());
        }

        private IEnumerator Flashing()
        {
            while (true)
            {
                if (FlashingEnabled)
                {
                    SoundManager.instance.PlayOneShotSound(SoundType.Snap);
                    Instantiate(ParticleSystemPrefab, ParticleSpawnPosition.transform.position, Quaternion.identity);
                }

                yield return new WaitForSeconds(Random.Range(TimeBetweenFlashesRange.x, TimeBetweenFlashesRange.y));
            }
        }
    }
}
