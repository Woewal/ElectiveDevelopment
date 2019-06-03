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

    [SerializeField]
    PlayerAttack attackComponent;
    [SerializeField]
    PlayerMovement movementComponent;
    [SerializeField]
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
