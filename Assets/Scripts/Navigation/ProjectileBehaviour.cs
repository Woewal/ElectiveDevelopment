using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class ProjectileBehaviour : MonoBehaviour
{
    public float timeToLive;
    public int damage;
    public Robot owner;


	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.GetComponent<Robot>())
        {
            var robot = other.gameObject.GetComponent<Robot>();
            if (robot.id != owner.id)
            {
                robot.health.takeDamage(damage);
                Destroy(this.gameObject);
            }
        }
        else if (other.tag.Equals("Wall"))
        {
            Destroy(gameObject);
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
