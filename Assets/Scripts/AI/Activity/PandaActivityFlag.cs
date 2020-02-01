using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.AI
{
    public class PandaActivityFlag : MonoBehaviour
    {
        public List<MonoBehaviour> CurrentActivities;

        public bool IsDuringActivity => CurrentActivities.Any();

        public void AddActivity(MonoBehaviour activity)
        {
            Assert.IsFalse(CurrentActivities.Contains(activity));
            CurrentActivities.Add(activity);
        }

        public void RemoveActivity(MonoBehaviour activity)
        {
            Assert.IsTrue(CurrentActivities.Contains(activity));
            CurrentActivities.Remove(activity);
        }
    }
}
