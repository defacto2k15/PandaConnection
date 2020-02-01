using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class DummyPanda : MonoBehaviour, IPanda
    {
        [SerializeField] public float Fullness;
        private PandaStats _stats;

        void Awake()
        {
            _stats = new PandaStats();
        }

        public PandaStats GetStats()
        {
            return _stats;
        }

        public float GetHealth()
        {
            throw new NotImplementedException();
        }

        public void SetHealth(float deltaHealth)
        {
            throw new NotImplementedException();
        }

        public float GetFullness()
        {
            return Fullness;
        }

        public void ChangeFullness(float deltaFood)
        {
            Fullness += deltaFood;
            Fullness = Mathf.Min(Fullness, GameManager.instance.pandaManager.GetMaximumFullness());
        }

        public void GetPrimaryColor()
        {
            throw new NotImplementedException();
        }

        public void GetSecondaryColor()
        {
            throw new NotImplementedException();
        }

        public void GetBodyPartSize(BodyPart part)
        {
            throw new NotImplementedException();
        }


        public float GetEro()
        {
            throw new NotImplementedException();
        }

        public void SetEro(float deltaEro)
        {
            throw new NotImplementedException();
        }
    }
}