using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AI;

[CreateAssetMenu]
public class AIKiril : Brain
{
    private SubjectiveRobot _ballOwner;
    private SubjectiveRobot _closestTeammate;
    private SubjectiveRobot _closestEnemy;

    private bool _noClosestTeammate;
    private bool _noClosestEnemy;

    private Vector3 _closestHealth;
    private Vector3 _closestSpeed;
    private Vector3 _closestInvis;
    private Vector3 _closestInvuln;


    private List<SubjectiveRobot> visibleEnemies = new List<SubjectiveRobot>();
    private List<SubjectiveRobot> visibleTeamMates = new List<SubjectiveRobot>();


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
        
    }


    private void CheckForVisibility(RobotControls controls)
    {
        visibleEnemies.Clear();
        visibleTeamMates.Clear();
        foreach(SubjectiveRobot robot in controls.archiveRobots)
        {
            if(robot.isSeen)
            {
                if(robot.team == controls.myself.team)
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

    private void UpdateClosestEnemy(RobotControls controls)
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
 
        if(closestEnemy.HasValue)
        {
            _closestEnemy = closestEnemy.Value;
            _noClosestEnemy = false;
        }
        else
        {
            _noClosestEnemy = true;
        }
    }

    private void UpdateClosestTeammate(RobotControls controls)
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


}

