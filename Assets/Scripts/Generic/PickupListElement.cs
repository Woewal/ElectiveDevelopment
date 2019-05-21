using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class PickupListElement : MonoBehaviour
{
    public Vector3 currentPickupPosition { get; private set; } = Vector3.zero;
    public string ofType { get; private set; } = null;
    public bool isSeen { get; private set; } = false;

    public PickupListElement(Pickup victim, bool canSee, bool isTeammate)
    {
        if(canSee || isTeammate)
        {
            // currentPickupPosition = victim.playerMovement.currentRobotPosition;
            // ofType = victim.ofType;
        }
        isSeen = canSee;
    }
}
