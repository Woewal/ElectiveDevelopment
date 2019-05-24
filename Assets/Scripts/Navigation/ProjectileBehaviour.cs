using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class ProjectileBehaviour : MonoBehaviour
{

    public float timeToLive;
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("player"))
        {
            collision.transform.parent.GetComponent<Robot>().DealDamage(damage);
            Destroy(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    private void Update()
    {
        if(timeToLive>0)
        {
            timeToLive -= Time.deltaTime;
        }
        else
            Destroy(this.gameObject);
    }
}
