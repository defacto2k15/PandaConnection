using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PandaLogic.Genetics
{
    public class DebugChildrenGeneticFun : MonoBehaviour
    {
        public ChildPandaCreator ChildPandaCreator;
        public PandaStats FatherStats;
        public PandaStats MotherStats;

        public PandaStats ChildStats;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChildStats = ChildPandaCreator.CreateChildStats(FatherStats, MotherStats);
            }
        }
    }
}
