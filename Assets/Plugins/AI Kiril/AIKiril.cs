using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using AI;

[CreateAssetMenu]
public class AIKiril : Brain
{
    //Single use bools
    private bool initialBallPosSet;

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
}

