using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Sounds
{
    public class AudioSourceAttendant : MonoBehaviour
    {
        public SoundType Type;
        public bool IsBackground;   
        public bool IsSustained;
        public GameObject PlayingObject;

        private float _timerResetMoment;
        private float _timerLength;

        void Awake()
        {
            if (IsSustained)
            {
                _timerLength = 1;
                StartCoroutine(TimerLoop());
            }
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