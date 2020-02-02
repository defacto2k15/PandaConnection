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
    [Action("MyActions/PandaIdle")]
    [Help("Rotate as idle for parametrized number of seconds")]
    public class PandaIdleAction : GOAction
    {
        [InParam("IdleDuration", DefaultValue = 30)]
        public float IdleDuration;

        private float _idleStartTime;

        public override void OnStart()
        {
            _idleStartTime = Time.time;
            gameObject.GetComponentNotNull<DummyPanda>().StartAnimationState(PandaAnimationState.Idle);
        }

        public override TaskStatus OnUpdate()
        {
            var idleElapsedTime = Time.time - _idleStartTime;
            if (idleElapsedTime < IdleDuration)
            {
                AnimatePanda(idleElapsedTime);
                return TaskStatus.RUNNING;
            }
            else
            {
                gameObject.GetComponentNotNull<DummyPanda>().StopAnimationState(PandaAnimationState.Idle);
                return TaskStatus.COMPLETED;
            }
        }

        private void AnimatePanda(float elapsedTime)
        {
            // gameObject.transform.RotateAround(gameObject.transform.position, gameObject.transform.up, Time.deltaTime*300);
        }
    }
}