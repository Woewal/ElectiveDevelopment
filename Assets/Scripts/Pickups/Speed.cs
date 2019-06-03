using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Pickup
{
	public float Duration;
	public float Amount;

	protected override void PickUp(Robot player, PickupHandler pickUpHandler)
	{
		player.GetComponent<PlayerMovement>().IncreaseSpeed(Amount, Duration);
	}
}
