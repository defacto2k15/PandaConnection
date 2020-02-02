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
                return TaskStatus.RUNNING;
            }
            else
            {
                return TaskStatus.COMPLETED;
            }
        }

        public override void OnEnd()
        {
            EndAnimation();
        }

        private void EndAnimation()
        {
            gameObject.GetComponentNotNull<DummyPanda>().StopAnimationState(PandaAnimationState.Idle);
        }

        public override TaskStatus OnLatentEnd()
        {
            EndAnimation();
            return base.OnLatentEnd();
        }

        public override TaskStatus OnLatentFailedEnd()
        {
            EndAnimation();
            return base.OnLatentFailedEnd();
        }

        public override void OnFailedEnd()
        {
            EndAnimation();
        }

        public override void OnAbort()
        {
            EndAnimation();
        }
    }
}