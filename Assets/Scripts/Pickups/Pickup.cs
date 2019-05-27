using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public abstract class Pickup : MonoBehaviour
{
	public enum Type { Ball, Health, Speed, Invincibility }

	public Type PickupType;

	public bool RespawnOnPickup;

	public Action<Player, PickupHandler> OnPickup = delegate { };

	void Awake()
	{
		OnPickup += (player, pickupHandler) =>
		{
			gameObject.SetActive(false);
			Spawner.Instance.Respawn(gameObject);
		};
		OnPickup += PickUp;
	}

	protected abstract void PickUp(Player player, PickupHandler pickupHandler);

	// private bool CanSee(GameObject target)
	// {
	// 	RaycastHit hit;
	// 	if (Physics.Raycast(playerMovement.currentRobotPosition, target.transform.position, out hit))
	// 	{
	// 		if (hit.transform.gameObject == target.gameObject)
	// 			return true;
	// 		else
	// 			return false;
	// 	}
	// 	else
	// 		return false;
	// }

}
