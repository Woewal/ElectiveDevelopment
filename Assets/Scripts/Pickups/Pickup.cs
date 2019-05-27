using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

//D
public abstract class Pickup : MonoBehaviour
{
	public enum Type { Ball, Health, Speed, Invincibility }

	public Type PickupType;

	abstract public void OnPickup(Player player, PickupHandler pickUpHandler);

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
