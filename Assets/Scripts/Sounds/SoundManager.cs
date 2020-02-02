using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public SoundManager instance;
        public List<AudioSourceAttendant> Prefabs;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void PlayOneShotSound(SoundType type)
        {
            var clipWithType = Prefabs.Single(c => c.Type == type);
            Assert.IsTrue(!clipWithType.IsBackground);
            Assert.IsTrue(!clipWithType.IsSustained);

            Instantiate(clipWithType, transform);
        }

        public void PlaySustainedTheme(GameObject playingObject, SoundType type, float playTimer)
        {
            var clipWithType = Prefabs.Single(c => c.Type == type);
            Assert.IsTrue(!clipWithType.IsBackground);
            Assert.IsTrue(clipWithType.IsSustained);

            var similarPlayingObjects = GetComponentsInChildren<AudioSourceAttendant>()
                .Where(c => c.Type == type && playingObject.Equals(c.PlayingObject)).ToList();
            Assert.IsTrue(similarPlayingObjects.Count <= 1);

            AudioSourceAttendant attendant;
            if (similarPlayingObjects.Any())
            {
                attendant = similarPlayingObjects.Single();
            }
            else
            {
                attendant = Instantiate(clipWithType, transform).GetComponentNotNull<AudioSourceAttendant>();
                attendant.PlayingObject = playingObject;
            }

            attendant.SustainTimer(playTimer);
        }

        public void PlayBackgroundTheme(SoundType type)
        {
            var clipWithType = Prefabs.Single(c => c.Type == type);
            Assert.IsTrue(clipWithType.IsBackground);
            Assert.IsTrue(!clipWithType.IsSustained);

            var currentBackgrounds = GetComponentsInChildren<AudioSourceAttendant>().ToList();
            Assert.IsTrue(currentBackgrounds.Count <=1 );

            if (currentBackgrounds.Count == 1)
            {
                currentBackgrounds.Single().StopAndDestroy();
            }

            Instantiate(clipWithType, transform);
        }
    }

    public enum SoundType
    {
        MenuAcceptBig,
        BackgroundTheme,
        Dying,
        Eating,
        MenuClick,
        OpeningTheme,
        Sex,
        Walking,
        Yay
    }
}
