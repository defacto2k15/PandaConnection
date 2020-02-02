using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.AI.Conditions;
using Assets.Scripts.Utils;
using BBUnity.Actions;
using Pada1.BBCore;
using UnityEngine;
using UnityEngine.AI;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;

namespace Assets.Scripts.AI.BehaviourActions
{
    [Action("MyActions/SetDestinationVectorAffectedByForceFields")]
    public class SetDestinationVectorAffectedByForceFieldsAction : GOAction
    {
        [OutParam("DestinationVector")] private Vector3 DestinationVector;

        private PandasActiveForceFieldContainer _fieldsContainer;

        public override void OnStart()
        {
            _fieldsContainer = gameObject.GetComponentNotNull<PandasActiveForceFieldContainer>();
        }

        public override TaskStatus OnUpdate()
        {
            DestinationVector = _fieldsContainer.ForceVector;
            return TaskStatus.COMPLETED;
        }
    }
}