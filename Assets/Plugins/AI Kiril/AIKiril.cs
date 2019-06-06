using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AI;

[CreateAssetMenu]
public class AIKiril : Brain
{

	private SubjectiveRobot _ballOwner;

	//Pickups locations
	private Vector3 _closestHealth;
	private Vector3 _closestSpeed;
	private Vector3 _closestInvis;
	private Vector3 _closestInvuln;

	//Ball ownership fields
	private Vector3 _lastBallPosition;
	private Vector3 _currentBallPosition;
	private bool _ballCaptured;
	private bool _enemyTeamHasTheBall;
	private bool _iHaveBall;

	//Single use bools
	private bool initialBallPosSet;

	private SubjectiveRobot currentTarget;
	private float projectileSpeed = 10f;
	private float targetSpeed;

	public override void UpdateAttack(RobotControls controls)
    {

    }

    public override void UpdateMovement(RobotControls controls)
    {

    }

    public override void UpdateBallPass(RobotControls controls)
    {

    }

    private void UpdateLocalData(RobotControls controls)
    {
        CheckForVisibility(controls);
        CheckBallOwnership(controls);
        CheckPickups(controls);
    }    

    /// <summary>
    /// This method calculates  Vector3 where projectile should be launced in order to hit the intended target;
    /// </summary>
    private Vector3 TargetPrediction(SubjectiveRobot target, RobotControls controls)
    {
        float coolDown = 0.5f;
        float coolDownVar = coolDown;

        Vector3 predictionPoint;

        Vector3 startPoint = target.currentPosition;
        while(coolDownVar > 0)
        {
            coolDown -= Time.deltaTime;
        }
        Vector3 endPoint = target.currentPosition;
        Vector3 targetMovementVector = endPoint - startPoint;
        targetSpeed = targetMovementVector.magnitude / coolDown;
        Vector3 distanceVector = endPoint - controls.myself.currentPosition;
        float distance = distanceVector.magnitude;
        float projection = Vector3.Project(targetMovementVector, distanceVector).magnitude;
        float timeToReach;
        timeToReach = (float)Math.Sqrt((Math.Pow(distance + projection, 2) + (Math.Pow(targetSpeed, 2) - Math.Pow(projection, 2))) / Math.Pow(projectileSpeed, 2));

        predictionPoint = targetMovementVector / (coolDown * timeToReach);
        return predictionPoint;
    }

	//Checks for pickups locations
	private void CheckPickups(RobotControls controls)
	{
		if (controls.updatePickup.Count > 0)
		{
			foreach (SubjectivePickup pickup in controls.updatePickup)
			{
				if (pickup.ofType == "Health")
				{
					if (_closestHealth != null)
					{
						if (Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition) < Vector3.Distance(_closestHealth, controls.myself.currentPosition))
						{
							_closestHealth = pickup.currentPickupPosition;
						}
					}
					else
					{
						_closestHealth = pickup.currentPickupPosition;
					}
				}
				else if (pickup.ofType == "Speed")
				{
					if (_closestSpeed != null)
					{
						if (Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition) < Vector3.Distance(_closestSpeed, controls.myself.currentPosition))
						{
							_closestSpeed = pickup.currentPickupPosition;
						}
					}
					else
					{
						_closestSpeed = pickup.currentPickupPosition;
					}
				}
				else if (pickup.ofType == "Invisibility")
				{
					if (_closestInvis != null)
					{
						if (Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition) < Vector3.Distance(_closestInvis, controls.myself.currentPosition))
						{
							_closestInvis = pickup.currentPickupPosition;
						}
					}
					else
					{
						_closestInvis = pickup.currentPickupPosition;
					}
				}
				else if (pickup.ofType == "Invulnerability")
				{
					if (_closestInvuln != null)
					{
						if (Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition) < Vector3.Distance(_closestInvuln, controls.myself.currentPosition))
						{
							_closestInvuln = pickup.currentPickupPosition;
						}
					}
					else
					{
						_closestInvuln = pickup.currentPickupPosition;
					}
				}
			}
		}
	}	

	//Checks who owns the ball
	private void CheckBallOwnership(RobotControls controls)
	{
		_currentBallPosition = controls.updateBall;
		_iHaveBall = false;
		_ballCaptured = false;
		foreach (SubjectiveRobot robot in controls.archiveRobots)
		{
			if (robot.team == controls.myself.team)
			{
				if (robot.currentPosition.x == _currentBallPosition.x && robot.currentPosition.y == _currentBallPosition.y)
				{
					_ballCaptured = true;
					_ballOwner = robot;
					_enemyTeamHasTheBall = false;
					if (robot.id == controls.myself.id)
					{
						_iHaveBall = true;
					}
					else
					{
						_iHaveBall = false;
					}
				}
			}
			else
			{
				if (robot.isSeen)
				{
					if (robot.currentPosition.x == _currentBallPosition.x && robot.currentPosition.y == _currentBallPosition.y)
					{
						_ballCaptured = true;
						_ballOwner = robot;
						_enemyTeamHasTheBall = true;
					}
				}
				else
				{
					if (_currentBallPosition != _lastBallPosition)
					{
						_ballCaptured = true;
						_enemyTeamHasTheBall = true;
					}
				}
			}
		}
		_lastBallPosition = _currentBallPosition;
	}
}

