using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Pickup
{
	public float Amount;

	protected override void PickUp(Robot player, PickupHandler pickUpHandler)
	{
		player.health.GainHealth(Amount);
	}
}
