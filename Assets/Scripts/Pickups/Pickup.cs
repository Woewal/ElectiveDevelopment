using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

//D
public abstract class Pickup : MonoBehaviour
{
	public enum Type { Ball, Health, Speed, Invisibility, Invulnerability }

	public Type PickupType;

	public bool RespawnOnPickup;

	public Action<Robot, PickupHandler> OnPickup = delegate { };

	void Awake()
	{
		if(RespawnOnPickup)
		{
			OnPickup += (player, pickupHandler) =>
			{
				gameObject.SetActive(false);
				Spawner.Instance.Respawn(gameObject);
			};
		}
		OnPickup += PickUp;
	}

	protected abstract void PickUp(Robot player, PickupHandler pickupHandler);

	public bool CanSee(GameObject target)
	{
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position, target.transform.position, out hit))
		{
			if (hit.transform.gameObject == target.gameObject)
				return true;
			else
				return false;
		}
		else
			return false;
	}

	public SubjectivePickup PickupToSubjectivePickup(Pickup pickup)
	{
		return new SubjectivePickup
		{
        currentPickupPosition = pickup.transform.position,
        ofType = pickup.PickupType.ToString()
		};
	}
}
