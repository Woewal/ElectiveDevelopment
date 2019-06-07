using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public class Ball : Pickup
{
	[HideInInspector] public Robot AssignedPlayer;
	[SerializeField] AnimationCurve _travelCurve;
	[SerializeField] float _travelSpeed;
	[SerializeField] float _maxHeight;

	Robot _ignoredPlayer;
	float _timeUntilPoint = 1;

	void Start()
    {
        BallManager.Instance.Register(this.gameObject);
    }

    void Update()
	{
		if (AssignedPlayer == null) return;

		_timeUntilPoint -= Time.deltaTime;

		if(_timeUntilPoint < 0)
		{
			AssignedPlayer.playerMovement.AddSlowDown();
            ScoreManager.Instance.OnScored(AssignedPlayer, 1);
			_timeUntilPoint = 1;
		}
	}

	protected override void PickUp(Robot player, PickupHandler pickupHandler)
	{
		if (player == _ignoredPlayer) return;


		BallManager.Instance.assignedPlayer = player;
		transform.localPosition = pickupHandler.transform.position + pickupHandler.ObjectHoldPosition;

		transform.SetParent(pickupHandler.transform);

		_timeUntilPoint = 1;
		AssignedPlayer = player;

		StopAllCoroutines();
	}

	public void Pass(Vector3 destination)
	{
		_ignoredPlayer = AssignedPlayer;
		AssignedPlayer = null;
		transform.SetParent(null);
		StartCoroutine(PassAnimation(destination));
	}

	public IEnumerator PassAnimation(Vector3 destination)
	{
		destination = new Vector3(destination.x, 0, destination.z);
		var startingPos = transform.position;
		var relativePos = destination - startingPos;
		var magnitude = relativePos.magnitude;
		float distanceTraveled = 0;

		while (distanceTraveled < magnitude)
		{

			transform.position = startingPos + relativePos.normalized * (magnitude * (distanceTraveled / magnitude))
				+ Vector3.up * _travelCurve.Evaluate(distanceTraveled/magnitude) * _maxHeight;
			distanceTraveled += Time.deltaTime * _travelSpeed;
			yield return null;
		}

		_ignoredPlayer = null;
	}

	public void Drop(PickupHandler pickupHandler)
	{
		pickupHandler.Ball = null;
		BallManager.Instance.assignedPlayer = null;
		AssignedPlayer.playerMovement.SlowDownStacks = 0;
		AssignedPlayer.playerMovement.UpdateMovementSpeed();
		AssignedPlayer = null;
		transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
		transform.SetParent(null);
	}
}
