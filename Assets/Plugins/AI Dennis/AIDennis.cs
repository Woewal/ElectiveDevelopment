using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class AIDennis : Brain
{
	SubjectivePickup? _closestHealth;
	SubjectivePickup? _closestInvisibilty;
	SubjectivePickup? _closestInvurnability;
	SubjectivePickup? _closestSpeed;

	Vector3 _ball;

	bool _hasBall;
	bool _teammateHasBall;
	bool _enemyHasBall;

	List<SubjectiveRobot> _allVisibleRobots;

	float _currentCooldown;
	float _cooldown = .3f;

	public override void UpdateData(RobotControls controls)
	{
		SetVisibleRobots(controls);
		AttemptBallFind(controls);
		CheckWhichTeamHasBall(controls);
	}

	public override void UpdateAttack(RobotControls controls)
	{
		if (_hasBall) return;

		_currentCooldown += Time.deltaTime;

		if (_currentCooldown < _cooldown) return;

		var _relativeDistance = _closestEnemy.currentPosition - controls.myself.currentPosition;

		if (_relativeDistance.magnitude > 3) return;

		Attack(_closestEnemy, controls);

		_currentCooldown = 0;
	}

	public override void UpdateBallPass(RobotControls controls)
	{
		if (!_hasBall) return;

		if (controls.myself.currentHealth < 30)
			controls.passBall(_closestTeammate.currentPosition);
	}

	public override void UpdateMovement(RobotControls controls)
	{
		if(_hasBall)
		{
			var relativeDistance = _closestEnemy.currentPosition - controls.myself.currentPosition;
			controls.goTo(controls.myself.currentPosition - relativeDistance);
		}
		else
		{
			controls.goTo(_ball);
		}
	}

	void Attack(SubjectiveRobot target, RobotControls controls)
	{
		controls.attack(target.currentPosition);
	}

	void SetVisibleRobots(RobotControls controls)
	{
		CheckForVisibility(controls);
		UpdateClosestEnemy(controls);
		UpdateClosestTeammate(controls);
		_allVisibleRobots = visibleTeamMates.Concat(visibleEnemies).ToList();
	}

	void AttemptBallFind(RobotControls controls)
	{ 
		_ball = controls.updateBall;
	}

	void CheckWhichTeamHasBall(RobotControls controls)
	{
		var allVisibleRobots = visibleTeamMates.Concat(visibleEnemies);

		foreach (var robot in allVisibleRobots)
		{
			if(robot.currentPosition.x == _ball.x && robot.currentPosition.z == _ball.z)
			{
				if(robot.team == controls.myself.team)
				{
					_teammateHasBall = true;
					_enemyHasBall = false;
					if (robot.id == controls.myself.id)
						_hasBall = true;
					else
						_hasBall = false;
				}
				else
				{
					_teammateHasBall = false;
					_enemyHasBall = true;
					_hasBall = false;
				}
			}
		}
	}

	void GetClosestPickups(RobotControls controls)
	{
		var pickups = controls.updatePickup;

		string[] pickupTypes = new string[] { "Health", "Speed", "Invisibility", "Invurnability" };
		SubjectivePickup?[] closestPickups = new SubjectivePickup?[] { _closestHealth, _closestSpeed, _closestInvisibilty, _closestInvurnability };

		for (int i = 0; i < pickupTypes.Length; i++)
		{
			SubjectivePickup? closestPickup = null;
			float minDist = Mathf.Infinity;
			Vector3 currentPos = controls.me.position;
			foreach (var pickup in pickups.Where(x => x.ofType == pickupTypes[i]))
			{
				float dist = Vector3.Distance(pickup.currentPickupPosition, currentPos);
				if (dist < minDist)
				{
					closestPickup = pickup;
					minDist = dist;
				}
			}
			closestPickups[i] = closestPickup;
		}
		
	}
}
