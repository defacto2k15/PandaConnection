using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaAnimationsCenter : MonoBehaviour
{
    public Animator pandaAnimator;
    public string idleAnimation;
    public string walkAnimation;
    public string eatAnimation;
    public string sexAnimation;

    public void StartIdle()
    {
        pandaAnimator.Play(idleAnimation);
    }

    public void StartEat()
    {
        pandaAnimator.Play(eatAnimation);
    }

    public void StartWalk()
    {
        pandaAnimator.Play(walkAnimation);
    }

    public void StartSexing()
    {
        pandaAnimator.Play(sexAnimation);
    }
}