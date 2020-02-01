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
        pandaAnimator.Play(eatAnimation);
        eatParticle.Emit(20);
    }

    public void StartWalk()
    {
        pandaAnimator.Play(walkAnimation);
    }

    public void StartSexing()
    {
        pandaAnimator.Play(sexAnimation);
        sexParticle.Emit(20);
    }

    public void StartDeath()
    {
        pandaAnimator.Play(deathAnimation);
        deathParticle.Emit(20);
    }
}