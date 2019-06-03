using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class RobotListElement : MonoBehaviour
{
    public Vector3 currentRobotPosition { get; private set; } = Vector3.zero;
    public float currentHealth { get; private set; } = 0f;
    public bool isAlive { get; private set; } = false;
    public bool teammate { get; private set; } = false;
    public bool isSeen { get; private set; } = false;
    public Vector3 lastShootVector { get; private set; } = Vector3.zero;

    public RobotListElement(Robot victim, bool canSee, bool isTeammate)
    {
        if(canSee || isTeammate)
        {
            currentRobotPosition = victim.playerMovement.currentRobotPosition;
            currentHealth = victim.health.CurrentHP;
            lastShootVector = victim.playerAttack.lastShootDirection;
        }
        teammate = isTeammate;
        isSeen = canSee;

    }
}
