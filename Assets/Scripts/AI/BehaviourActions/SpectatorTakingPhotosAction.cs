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
    [Action("MyActions/SpectatorTakingPhotosAction")]
    [Help("Rotate as idle for parametrized number of seconds")]
    public class SpectatorTakingPhotosAction : GOAction
    {
        [InParam("Duration", DefaultValue = 30)]
        public float Duration;

        private SpectatorCameraFlashing _flashing;
        private float _idleStartTime;

        public override void OnStart()
        {
            _idleStartTime = Time.time;
            _flashing =gameObject.GetComponentNotNull<SpectatorCameraFlashing>();
        }

        public override TaskStatus OnUpdate()
        {
            var elapsedTime = Time.time - _idleStartTime;
            _flashing.FlashingEnabled = true;
            if (elapsedTime < Duration)
            {
                return TaskStatus.RUNNING;
            }
            else
            {
                _flashing.FlashingEnabled =false;
                return TaskStatus.COMPLETED;
            }
        }
    }
}