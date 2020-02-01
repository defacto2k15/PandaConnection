using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

namespace Assets.Scripts.AI.Conditions
{
    [Condition("MyCondition/PandaIsDuringActivity")]
    public class  PandaIsDuringActivityCondition: ConditionBase
    {
        [InParam("ThisPanda")]
        GameObject ThisPanda;

        public override bool Check()
        {
            var container = ThisPanda.GetComponentNotNull<PandaActivityFlag>();
            return container.IsDuringActivity;
        }
    }
}
