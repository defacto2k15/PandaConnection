using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using BBUnity.Actions;
using Pada1.BBCore;
using UnityEngine;
using UnityEngine.AI;
using TaskStatus = Pada1.BBCore.Tasks.TaskStatus;

namespace Assets.Scripts.AI.BehaviourActions
{
    [Action("MyActions/MoveInDirection")]
    public class MoveInDirectionAction : GOAction
    {
        [InParam("DestinationVector")]
        Vector3 DestinationVector;

        [InParam("SuccessDistance")] float SuccessDistance;

        private NavMeshAgent _agent;

        private Vector3 _destinationPosition; 
        

        public override void OnStart()
        {
            _agent = gameObject.GetComponentNotNull<NavMeshAgent>();
            CreateGoal();
        }

        private void CreateGoal()
        {
            _destinationPosition = gameObject.transform.position + DestinationVector.normalized * SuccessDistance;
            _agent.SetDestination(_destinationPosition);
            _agent.isStopped = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (_agent.isStopped)
            {
                CreateGoal();
            }
            if (_agent.remainingDistance < SuccessDistance)
            {
                return TaskStatus.COMPLETED;
            }
            else
            {
                return TaskStatus.RUNNING;
            }
        }
    }
}
