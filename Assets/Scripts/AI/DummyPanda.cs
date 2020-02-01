using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class DummyPanda : MonoBehaviour,  IPanda
    {
        public PandaStats GetStats()
        {
            throw new NotImplementedException();
        }

        public float GetHealth()
        {
            throw new NotImplementedException();
        }

        public void SetHealth(float deltaHealth)
        {
            throw new NotImplementedException();
        }

        public float GetFood()
        {
            throw new NotImplementedException();
        }

        public void SetFood(float deltaFood)
        {
            throw new NotImplementedException();
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
    }
}
