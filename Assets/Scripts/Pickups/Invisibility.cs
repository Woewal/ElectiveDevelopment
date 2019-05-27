using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : Pickup
{
	public float Duration;

	protected override void PickUp(Player player, PickupHandler pickUpHandler)
	{
		player.GetComponent<Health>().GainInvisisbility(Duration);
	}
}
