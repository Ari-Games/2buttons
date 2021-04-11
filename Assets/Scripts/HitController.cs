using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordEffect{
    frameball,ereikball,fireshot,whiteshield
}
public class HitController : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> swordEffects;
    public void AnimateOnParticle(SwordEffect swordEffect)
    {
        int index = (int)swordEffect;
        if (!swordEffects[index].isPlaying)
        {
            swordEffects[index].Play();
        }        
 
        
    }
    public void AnimateOffParticle(SwordEffect swordEffect)
    {
        swordEffects[(int)swordEffect].Stop();

    }
}
