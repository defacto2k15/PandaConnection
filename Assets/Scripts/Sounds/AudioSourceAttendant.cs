using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceAttendant : MonoBehaviour
    {
        public SoundType Type;
        public bool IsBackground;   
        public bool IsSustained;
        public GameObject PlayingObject;

        private float _timerResetMoment;
        private float _timerLength;
        private AudioSource _source; 

        void Awake()
        {
            _timerResetMoment = Time.time;
            _source = this.GetComponentNotNull<AudioSource>();
            if (IsSustained)
            {
                _timerLength = 1;
                StartCoroutine(TimerLoop());
            }
            else
            { 
                StartCoroutine(DestroyAtEnd());
            }
        }

        private IEnumerator DestroyAtEnd()
        {
            yield return new WaitForSeconds(_source.clip.length);
            StopAndDestroy();
        }

        public void StopAndDestroy()
        {
            GameObject.Destroy(gameObject);
        }

        public void SustainTimer(float timerLength)
        {
            Assert.IsTrue(IsSustained);
            _timerLength = timerLength;
            _timerResetMoment = Time.time;
        }

        private IEnumerator TimerLoop()
        {
            while (Time.time - _timerResetMoment < _timerLength)
            {
                yield return new WaitForSeconds(_timerLength);
            }
            StopAndDestroy();
        }
    }
}