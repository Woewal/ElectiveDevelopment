using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public abstract class Pickup : MonoBehaviour
{
	public enum Type { Ball, Health, Speed, Invincibility }

	public Type PickupType;

	abstract public void OnPickup(Player player, PickupHandler pickUpHandler);

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
