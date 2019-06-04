using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class AIDennis : Brain
{
	public override void UpdateAttack(RobotControls controls)
	{
		//Debug.Log("Updating attack");
		//controls.attack(GetClosestEnemy(controls));
	}

	public override void UpdateBallPass(RobotControls controls)
	{
		//Debug.Log("Updating ball pass");
		//var closestEnemy = GetClosestEnemy(controls);

		//if (closestEnemy == Vector3.zero)
		//	return;

		//controls.goTo(controls.me.position - closestEnemy);
	}

	public override void UpdateMovement(RobotControls controls)
	{
		Debug.Log("Updating movement");
		controls.goTo(controls.updateBall);
	}

	List<SubjectiveRobot> GetVisibleRobots(RobotControls controls)
	{
		var robots = controls.archiveRobots.Where(x => x.isSeen).ToList();
		return robots;
	}

	Vector3 GetClosestEnemy(RobotControls controls)
	{
		var robots = GetVisibleRobots(controls);
		
		Vector3 tMin = Vector3.zero;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = controls.me.position;
		foreach (var t in robots)
		{
			float dist = Vector3.Distance(t.currentPosition, currentPos);
			if (dist < minDist)
			{
				tMin = t.currentPosition;
				minDist = dist;
			}
		}
		return tMin;
	}
}
