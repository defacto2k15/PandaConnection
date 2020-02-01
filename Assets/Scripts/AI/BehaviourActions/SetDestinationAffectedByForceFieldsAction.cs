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
    public class SetDestinationAffectedByForceFieldsAction : GOAction
    {
        [InParam("StepLength")] float StepLength;

        [OutParam("Destination")] Vector3 Destination;

        private PandasActiveForceFieldContainer _fieldsContainer;
        //private NavMeshAgent _agent;

        public override void OnStart()
        {
            _fieldsContainer = gameObject.GetComponentNotNull<PandasActiveForceFieldContainer>();
            //_agent = gameObject.GetComponentNotNull<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            //_agent.destination = _fieldsContainer.ForceVector * StepLength;
            Destination = _fieldsContainer.ForceVector * StepLength;
            return TaskStatus.COMPLETED;
        }
    }
}
