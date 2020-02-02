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
        [SerializeField] public float Ero;
        [SerializeField] public float Health;
        [SerializeField] PandaStats _stats;

        public PandaStats GetStats()
        {
            return _stats;
        }

        public void SetStats(PandaStats stats)
        {
            _stats = stats;
        }

        public float GetHealth()
        {
            return Health;
        }

        public void ChangeHealth(float deltaHealth)
        {
            Health += deltaHealth;
            Health = Mathf.Clamp(Health, 0, GameManager.instance.pandaManager.GetMaximumHealth());
        }

        public float GetFullness()
        {
            return Fullness;
        }

        public void ChangeFullness(float deltaFood)
        {
            Fullness += deltaFood;
            Fullness = Mathf.Clamp(Fullness,0, GameManager.instance.pandaManager.GetMaximumFullness());
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

        public bool IsNotFull()
        {
            return GetFullness() < GameManager.instance.pandaManager?.GetMaximumFullness();
        }

        public Gender GetGender()
        {
            return _stats.gender;
        }

        public float GetEro()
        {
            return Ero;
        }

        public void ChangeEro(float deltaEro)
        {
            Ero += deltaEro;
            Ero = Mathf.Clamp(Ero,0, GameManager.instance.pandaManager.GetMaximumEro());
        }
    }
}