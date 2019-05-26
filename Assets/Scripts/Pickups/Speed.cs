using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Pickup
{
	public float Duration;
	public float Amount;

	public override void OnPickup(Player player, PickupHandler pickUpHandler)
	{
		player.GetComponent<PlayerMovement>().IncreaseSpeed(Amount, Duration);
	}
}
