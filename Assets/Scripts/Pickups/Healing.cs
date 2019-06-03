using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Pickup
{
	public float Amount;

	protected override void PickUp(Player player, PickupHandler pickUpHandler)
	{
		player.GetComponent<Health>().GainHealth(Amount);
	}
}
