﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public class PickupHandler : MonoBehaviour
{
	public Vector3 ObjectHoldPosition;
	public Ball Ball;

	Player _player;

	void OnTriggerEnter(Collider other)
	{
		Pickup pickup = other.GetComponent<Pickup>();

		if (pickup == null)
			return;

		if (_player == null)
			_player = GetComponent<Player>();

		pickup.OnPickup(_player, this);

		if (pickup.GetType() == typeof(Ball))
		{
			Ball = (Ball)pickup;
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if (Ball == null) return;

			Ball.Pass(transform.position + new Vector3(0, 0, 5));
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position + ObjectHoldPosition, .1f);
	}
}
