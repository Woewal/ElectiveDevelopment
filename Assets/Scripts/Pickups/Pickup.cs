using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//D
public abstract class Pickup : MonoBehaviour
{
	abstract public void OnPickup(Player player, PickupHandler pickUpHandler);
}
