using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class ProjectileBehaviour : MonoBehaviour
{
    public float timeToLive;
    public int damage;

	void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

	void OnTriggerEnter(Collider other)
	{
		var robot = other.gameObject.GetComponent<Robot>();

		if (robot != null)
		{
			robot.health.takeDamage(damage);
			Destroy(this.gameObject);
		}
	}

	void Update()
    {
        if(timeToLive>0)
        {
            timeToLive -= Time.deltaTime;
        }
        else
            Destroy(this.gameObject);
    }
}
