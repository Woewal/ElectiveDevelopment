using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public class Ball : Pickup
{
	[HideInInspector] public Player AssignedPlayer;

	[SerializeField] float _travelSpeed;

	Player _ignoredPlayer;
	float _timeUntilPoint = 1;

	void Update()
	{
		if (AssignedPlayer == null) return;

		_timeUntilPoint -= Time.deltaTime;

		if(_timeUntilPoint < 0)
		{
			//TODO add point;
			_timeUntilPoint = 1;
		}
	}

	public override void OnPickup(Player player, PickupHandler pickupHandler)
	{
		if (player == _ignoredPlayer) return;

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
			transform.position = startingPos + relativePos.normalized * (magnitude * (distanceTraveled / magnitude));
			distanceTraveled += Time.deltaTime * _travelSpeed;
			yield return null;
		}

		_ignoredPlayer = null;
	}

	public void Drop()
	{
		AssignedPlayer = null;
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		transform.SetParent(null);
	}
}
