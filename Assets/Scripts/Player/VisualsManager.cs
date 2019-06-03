using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsManager : MonoBehaviour
{

    public ParticleSystem getDamage;
    public ParticleSystem getHealed;
    public ParticleSystem speedEffect;
    public ParticleSystem invisEffect;
    public ParticleSystem immortalEffect;


    public void StartEffect(int effectNumber)
    {
        StartEffect(effectNumber, 0f);
    }
    public void StartEffect(int effectNumber, float duration)
    {
        if (duration <= 0)
        {
            StartCoroutine(StartVisualEffect(effectNumber, 1f));
        }
        else
        {
            StartCoroutine(StartVisualEffect(effectNumber, duration));
        }
    }
    
    private IEnumerator StartVisualEffect(int effectNumber, float time)
    {
        switch (effectNumber)
        {
            case 1:
                speedEffect.Play();
                break;
            case 2:
                invisEffect.Play();
                break;
            case 3:
                immortalEffect.Play();
                break;
            case 4:
                getDamage.Play();
                break;
            case 5:
                getHealed.Play();
                break;
            default:
                break;

        }
        yield return new WaitForSeconds(time);
        switch (effectNumber)
        {
            case 1:
                speedEffect.Stop();
                break;
            case 2:
                invisEffect.Stop();
                break;
            case 3:
                immortalEffect.Stop();
                break;
            default:
                break;
        }
    }
}
