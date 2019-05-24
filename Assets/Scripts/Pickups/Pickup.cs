using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public abstract class Pickup : MonoBehaviour
{
	public enum Type { Ball, Health, Speed, Invincibility }

	public Type PickupType;

	abstract public void OnPickup(Player player, PickupHandler pickUpHandler);

}
