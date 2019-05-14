using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    Transform testTarget;

    [SerializeField]
    GameObject projectile;

    public float attackCoolDown;
    public float projectileSpeed;

    private float coolDownVariable;
    private Vector3 lastTarget;

    public Transform weaponHolder;
    // Start is called before the first frame update
    void Start()
    {
        coolDownVariable = attackCoolDown;
        weaponHolder = GetComponent<Transform>();
    }

    public void Shoot(Vector3 _target)
    {
        if (coolDownVariable > 0)
            return;
        Vector3 shootDir = (_target - weaponHolder.position).normalized;
        GameObject bullet = Instantiate(projectile ,weaponHolder.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = shootDir * projectileSpeed;

        coolDownVariable = attackCoolDown;

    }

    // Update is called once per frame
    void Update()
    {
        if(coolDownVariable > 0)
        {
            coolDownVariable -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Shoot(testTarget.position);
        }
    }

}
