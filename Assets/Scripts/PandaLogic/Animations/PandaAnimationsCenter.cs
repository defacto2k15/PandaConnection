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
    public string deathAnimation;
    public ParticleSystem sexParticle;
    public ParticleSystem eatParticle;
    public ParticleSystem deathParticle;

    public void StartIdle()
    {
        pandaAnimator.Play(idleAnimation);
    }

    public void StartEat()
    {
        Debug.Log("StartEat animation");
        if (pandaAnimator != null)
        {
            pandaAnimator.Play(eatAnimation);
        }
        eatParticle.Emit(20);
    }

    public void StartWalk()
    {
        if (pandaAnimator != null)
        {
            pandaAnimator.Play(walkAnimation);
        }
        Debug.Log("StartWalk animation");
    }

    public void StartSexing()
    {
        Debug.Log("StartSexing animation");
        if (pandaAnimator != null)
        {
            pandaAnimator.Play(sexAnimation);
        }
        sexParticle.Emit(20);
    }

    public void StartDeath()
    {
        Debug.Log("StartDeath animation");
        if (pandaAnimator != null)
        {
            pandaAnimator.Play(deathAnimation);
        }
        deathParticle.Emit(20);
    }
}