using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //Visual management
    VisualsManager visuals;

    public Vector3 lastDestination;
    private NavMeshAgent navMeshAgent;

    private Transform robotTransform;
    public Vector3 currentRobotPosition { get; private set; }
	// Start is called before the first frame update

	public int SlowDownStacks;
	public float SlowDownPerStack;
	public bool isInvisible;

	float moveSpeed;
	
    void Start()
    {
        visuals = GetComponent<VisualsManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        robotTransform = this.GetComponent<Transform>();
		moveSpeed = navMeshAgent.speed;
    }

    public void MoveTowards(Vector3 _target)
    {
        lastDestination = _target;
        navMeshAgent.SetDestination(lastDestination);
    }
    // Update is called once per frame
    void Update()
    {
        currentRobotPosition = robotTransform.position;
    }

	public void AddSlowDown()
	{
		SlowDownStacks++;
		UpdateMovementSpeed();
	}

	public void UpdateMovementSpeed()
	{
		navMeshAgent.speed = moveSpeed - SlowDownStacks * SlowDownPerStack;
	}

	public void IncreaseSpeed(float amount, float duration)
	{
		StopAllCoroutines();
		StartCoroutine(IncreaseSpeedCoroutine(amount, duration));
        visuals.StartEffect(1, duration);
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

	public void GainInvisibility(float duration)
	{
		StopAllCoroutines();
		StartCoroutine(GainInvisibilityCoroutine(duration));
        visuals.StartEffect(2, duration);
		// player.child(1).child(1+2).albeto(transparent)
	}

	IEnumerator GainInvisibilityCoroutine(float duration)
	{
		float currentTime = 0;
		isInvisible = true;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			yield return null;
		}

		isInvisible = false;
	}
}
