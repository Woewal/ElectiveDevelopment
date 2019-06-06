using System.Collections.Generic;
using System;
using UnityEngine;

namespace AI
{
	public struct Target
	{
		public Vector3 position;
		public Quaternion rotation;
		public float currentHealth;
		public bool alive;
		public Team team;
	}

	public struct SubjectiveRobot
	{
		public Vector3 currentPosition;
		public float currentHealth;
		public bool isAlive;
		public bool isSeen;
		public Vector3 lastShootDir;
		//public Team team;
		public int team;
		public int id;
	}

	public struct SubjectivePickup
	{
		public Vector3 currentPickupPosition;
		public string ofType;
	}

	public struct RobotControls
	{
		#region Actions

		public Action<Vector3> goTo;
		public Action<Vector3> attack;
		public Action<Vector3> passBall;

		#endregion

        #region Data
        //new Data

        public float reload;
        public SubjectiveRobot myself;
        public Vector3 updateBall;

		//public List<SubjectiveRobot> updateRobots;
		//public List<SubjectiveRobot> archiveRobots;
		public List<SubjectiveRobot> archiveRobots;

		public List<SubjectivePickup> updatePickup;

		public Target me;
		// public Team team;

		public Target[] visibleTargets;

		#endregion
	}

	public abstract class Brain : ScriptableObject
	{
		public virtual void UpdateData(RobotControls controls) { }
		public abstract void UpdateAttack(RobotControls controls);
		public abstract void UpdateMovement(RobotControls controls);
		public abstract void UpdateBallPass(RobotControls controls);

		
		protected SubjectiveRobot _closestTeammate;
		protected SubjectiveRobot _closestEnemy;

		protected bool _noClosestTeammate;
		protected bool _noClosestEnemy;

		protected List<SubjectiveRobot> visibleEnemies = new List<SubjectiveRobot>();
		protected List<SubjectiveRobot> visibleTeamMates = new List<SubjectiveRobot>();

		protected void UpdateClosestEnemy(RobotControls controls)
		{
			SubjectiveRobot? closestEnemy;
			closestEnemy = null;
			float distanceMin = 1000f;

			if (visibleEnemies.Count > 0)
			{
				foreach (SubjectiveRobot robot in visibleEnemies)
				{
					if (Vector3.Distance(robot.currentPosition, controls.myself.currentPosition) < distanceMin)
					{
						distanceMin = Vector3.Distance(robot.currentPosition, controls.myself.currentPosition);
						closestEnemy = robot;
					}
				}
			}

			if (closestEnemy.HasValue)
			{
				_closestEnemy = closestEnemy.Value;
				_noClosestEnemy = false;
			}
			else
			{
				_noClosestEnemy = true;
			}
		}

		protected void CheckForVisibility(RobotControls controls)
		{
			visibleEnemies.Clear();
			visibleTeamMates.Clear();
			foreach (SubjectiveRobot robot in controls.archiveRobots)
			{
				if (robot.isSeen)
				{
					if (robot.team == controls.myself.team)
					{
						visibleTeamMates.Add(robot);
					}
					else
					{
						visibleEnemies.Add(robot);
					}
				}
			}
		}

		protected void UpdateClosestTeammate(RobotControls controls)
		{
			SubjectiveRobot? closestTeammate;
			closestTeammate = null;
			float distanceMin = 1000f;

			if (visibleEnemies.Count > 0)
			{
				foreach (SubjectiveRobot robot in visibleEnemies)
				{
					if (Vector3.Distance(robot.currentPosition, controls.myself.currentPosition) < distanceMin)
					{
						distanceMin = Vector3.Distance(robot.currentPosition, controls.myself.currentPosition);
						closestTeammate = robot;
					}
				}
			}

			if (closestTeammate.HasValue)
			{
				_closestTeammate = closestTeammate.Value;
				_noClosestTeammate = false;
			}
			else
			{
				_noClosestTeammate = true;
			}
		}




	}
}
