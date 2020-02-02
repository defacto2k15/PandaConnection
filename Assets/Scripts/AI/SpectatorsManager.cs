using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.AI
{
    public class SpectatorsManager : MonoBehaviour
    {
        public GameObject SpectatorPerfab;
        public GameObject PhotoArea;
        public GameObject SpectatorGoal;
        public GameObject SpectatorsSpawnPoint;
        public int MaxSpectators;
        public float TimeBetweenSpectatorSpawningMoments;
        public float SpectatorSpawnChance;

        void Start()
        {
            StartCoroutine(SpectatorSpawningCoroutine());
        }

        private IEnumerator SpectatorSpawningCoroutine()
        {
            while (true)
            {
                if (FindObjectsOfType<SpectatorCameraFlashing>().Length < MaxSpectators)
                {
                    if (UnityEngine.Random.Range(0, 1) < SpectatorSpawnChance)
                    {
                        var spectator = Instantiate(SpectatorPerfab, SpectatorsSpawnPoint.transform.position, SpectatorsSpawnPoint.transform.rotation);
                        var behaviourExecutor = spectator.GetComponentNotNull<BehaviorExecutor>();
                        behaviourExecutor.SetBehaviorParam("photoArea", PhotoArea);
                        behaviourExecutor.SetBehaviorParam("spectatorGoal", SpectatorGoal);
                    }
                }
                yield return new WaitForSeconds(TimeBetweenSpectatorSpawningMoments);
            }
        }
    }
}
