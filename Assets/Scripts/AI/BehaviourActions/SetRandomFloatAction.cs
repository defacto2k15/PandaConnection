using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Random = UnityEngine.Random;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;

namespace Assets.Scripts.AI.BehaviourActions
{
    [Action("MyActions/RandomFloat")]
    public class SetRandomFloatAction : BasePrimitiveAction
    {
        [InParam("Minimum")] public float Minimum;
        [InParam("Maximum")] public float Maximum;

        [OutParam("Output")] public float Output;

        public override TaskStatus OnUpdate()
        {
            Output = Random.Range(Minimum, Maximum);
            return TaskStatus.COMPLETED;
        }
    }
}
