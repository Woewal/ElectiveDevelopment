using AI;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance;
	public List<Robot> robots = new List<Robot>();
	public CinemachineVirtualCamera Camera;
	public Transform Transform;
	public Transform TargetTransform;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		RobotManager.Instance.OnRobotAdded += AddTarget;
		StartCoroutine(Wait(5));
	}

	private void Update()
	{
		if (TargetTransform != null)
			Transform.position = TargetTransform.position;
	}

	public void AddTarget(Robot robot)
	{
		robots.Add(robot);
	}

	IEnumerator Wait(float duration)
	{
		yield return new WaitForSeconds(.2f);

		ChangeCameraPosition();

		while (true)
		{
			float currentTime = 0;
			while (currentTime < duration)
			{
				currentTime += Time.deltaTime;
				yield return null;
			}
			ChangeCameraPosition();
			yield return null;
		}
	}

	public void ChangeCameraPosition()
	{
		if (BallManager.Instance.assignedPlayer != null)
			TargetTransform = BallManager.Instance.assignedPlayer.transform;
		else
		{
			var randomIndex = Random.Range(0, robots.Count);
			TargetTransform = robots[randomIndex].transform;
		}
	}
}
