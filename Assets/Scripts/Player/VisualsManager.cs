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

    public GameObject head;
    public GameObject body;


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
                head.GetComponent<MeshRenderer>().materials[0].color = new Vector4(1, 1, 1, 0.29f);
                body.GetComponent<MeshRenderer>().materials[0].color = new Vector4(1, 1, 1, 0.29f);
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
                head.GetComponent<MeshRenderer>().materials[0].color = new Vector4(1, 1, 1, 1);
                body.GetComponent<MeshRenderer>().materials[0].color = new Vector4(1, 1, 1, 1);
                break;
            case 3:
                immortalEffect.Stop();
                break;
            default:
                break;
        }
    }
}
