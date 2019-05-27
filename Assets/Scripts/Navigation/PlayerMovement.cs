using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 lastDestination;
    private NavMeshAgent navMeshAgent;

    private Transform robotTransform;
    public Vector3 currentRobotPosition { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        robotTransform = this.GetComponent<Transform>();
    }

    public void MoveTowards(Vector3 _target)
    {
        lastDestination = _target;
        navMeshAgent.SetDestination(lastDestination);
        Debug.Log(navMeshAgent);
        Debug.Log(lastDestination);
    }
    // Update is called once per frame
    void Update()
    {
        currentRobotPosition = robotTransform.position;
    }

	public void IncreaseSpeed(float amount, float duration)
	{
		StopAllCoroutines();
		StartCoroutine(IncreaseSpeedCoroutine(amount, duration));
	}

	IEnumerator IncreaseSpeedCoroutine(float amount, float duration)
	{
		navMeshAgent.speed += amount;
		float currentTime = 0;

		while(currentTime < duration)
		{
			currentTime += Time.deltaTime;
			yield return null;
		}

		navMeshAgent.speed -= amount;
	}
}
