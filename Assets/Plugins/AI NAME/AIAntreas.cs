using AI;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

[CreateAssetMenu]
public class AIAntreas : Brain
{
    public Vector3 destinationPoint;

    public Vector3 powerupPos1 = Vector3.zero;
    public Vector3 powerupPos2 = Vector3.zero;
    public Vector3 powerupPos3 = Vector3.zero;
    public Vector3 powerupPos4 = Vector3.zero;

    public float curHealthThreshold = 40;

    bool powerupsVisible;
    bool powerupsRecalled;
    bool alliesVisible;
    bool opponentsVisible;
    bool lowOnHealth;

    public override void UpdateData(RobotControls controls){}

    public override void UpdateMovement(RobotControls controls)
    {
        #region Ball carrier exists

            if(GetBallCarrier(controls) != null)
            {
                #region I am ball carrier

                    if(GetBallCarrier(controls).Value.team == controls.myself.team
                    && GetBallCarrier(controls).Value.id == controls.myself.id)
                    {
                        #region Powerup Exists

                            powerupsVisible = controls.updatePickup.Count > 0;
                            powerupsRecalled = RecallPowerPositions(controls) != Vector3.zero;
                            alliesVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, true, true) != null;
                            opponentsVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, false, true) != null;
                            lowOnHealth = controls.myself.currentHealth > curHealthThreshold;

                            if((powerupsVisible || powerupsRecalled) && (!lowOnHealth || !alliesVisible) && opponentsVisible)
                            {
                                //Move towards closest powerup
                                GetClosestPowerup(controls, controls.myself.currentPosition);
                                //Shoot closest opponent
                                if (controls.reload == 0){}
                            }

                        #endregion

                        #region Evade closest opponent

                            powerupsVisible = controls.updatePickup.Count > 0;
                            powerupsRecalled = RecallPowerPositions(controls) != Vector3.zero;
                            alliesVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, true, true) != null;
                            opponentsVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, false, true) != null;
                            lowOnHealth = controls.myself.currentHealth > curHealthThreshold;

                            if((!powerupsVisible && !powerupsRecalled) && (!lowOnHealth || !alliesVisible) && opponentsVisible)
                            {
                                //Move away from closest opponent
                                GetSeenRobotTarget(controls, controls.myself.currentPosition, false, true);
                                //Shoot closest opponent
                            }

                        #endregion

                        #region Pass the ball

                            powerupsVisible = controls.updatePickup.Count > 0;
                            powerupsRecalled = RecallPowerPositions(controls) != Vector3.zero;
                            alliesVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, true, true) != null;
                            opponentsVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, false, true) != null;
                            lowOnHealth = controls.myself.currentHealth > curHealthThreshold;

                            if((lowOnHealth && alliesVisible) && opponentsVisible)
                            {
                                //Pass the ball to furthest ally
                                GetSeenRobotTarget(controls, controls.myself.currentPosition, true, false);
                            }

                        #endregion

                        #region Coast clear

                            powerupsVisible = controls.updatePickup.Count > 0;
                            powerupsRecalled = RecallPowerPositions(controls) != Vector3.zero;
                            alliesVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, true, true) != null;
                            opponentsVisible = GetSeenRobotTarget(controls, controls.myself.currentPosition, false, true) != null;
                            lowOnHealth = controls.myself.currentHealth > curHealthThreshold;

                            if(!opponentsVisible)
                            {
                                //Stop moving
                            }

                        #endregion
                    }

                #endregion
                
                #region Ally ball

                    else if(GetBallCarrier(controls).Value.team == controls.myself.team)
                    {
                        //Move towards ball
                        controls.goTo(controls.updateBall);
                        //Shoot at closest opponent to ball carrier if any
                    }

                #endregion

                #region Opponent ball

                    else if(GetBallCarrier(controls).Value.team != controls.myself.team)
                    {
                        //Move towards ball
                        controls.goTo(controls.updateBall);
                        //Shoot at ball carrier
                    }

                #endregion
            }

        #endregion

        #region Anybody's ball

            else if(GetBallCarrier(controls) == null)
            {
                //Move towards ball
                controls.goTo(controls.updateBall);
                //Shoot at closest opponent if any
            }

        #endregion
    }

    public override void UpdateAttack(RobotControls controls)
    {
        
    }

    public override void UpdateBallPass(RobotControls controls)
    {
        
    }

    public SubjectiveRobot? GetSeenRobotTarget(RobotControls controls, Vector3 startPos, bool alliesOpponents, bool closestFurthest)
    {
        SubjectiveRobot? target = null;
        float minDistance = Mathf.Infinity;
        float maxDistance = 0;
        if(controls.archiveRobots.Count > 1)
        {
            foreach (SubjectiveRobot robot in controls.archiveRobots)
            {
                Vector3 diff = robot.currentPosition - startPos;
                float curDistance = diff.sqrMagnitude;
                if (robot.isSeen && robot.id != controls.myself.id)
                {
                    if (alliesOpponents && robot.team == controls.myself.team)
                    {
                        if (closestFurthest && curDistance < minDistance)
                        {
                            target = robot;
                            minDistance = curDistance;
                        }
                        if (!closestFurthest && curDistance > maxDistance)
                        {
                            target = robot;
                            maxDistance = curDistance;
                        }
                    }
                    if (!alliesOpponents && robot.team != controls.myself.team)
                    {
                        if (closestFurthest && curDistance < minDistance)
                        {
                            target = robot;
                            minDistance = curDistance;
                        }
                        if (!closestFurthest && curDistance > maxDistance)
                        {
                            target = robot;
                            maxDistance = curDistance;
                        }
                    }
                }
            }
        }
        return target;
    }

    public SubjectivePickup? GetClosestPowerup(RobotControls controls, Vector3 startPos)
    {
        SubjectivePickup? powerupTarget = null;
        float minDistance = Mathf.Infinity;
        if(controls.updatePickup.Count > 0)
        {
            foreach (SubjectivePickup pickup in controls.updatePickup)
            {
                Vector3 diff = pickup.currentPickupPosition - startPos;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < minDistance)
                {
                    powerupTarget = pickup;
                    minDistance = curDistance;
                }
                if (pickup.currentPickupPosition != powerupPos1 && pickup.currentPickupPosition != powerupPos2
                && pickup.currentPickupPosition != powerupPos3 && pickup.currentPickupPosition != powerupPos4)
                {
                    if (powerupPos1 == Vector3.zero)
                    {
                        powerupPos1 = pickup.currentPickupPosition;
                    }
                    else if (powerupPos2 == Vector3.zero)
                    {
                        powerupPos2 = pickup.currentPickupPosition;
                    }
                    else if (powerupPos3 == Vector3.zero)
                    {
                        powerupPos3 = pickup.currentPickupPosition;
                    }
                    else if (powerupPos4 == Vector3.zero)
                    {
                        powerupPos4 = pickup.currentPickupPosition;
                    }
                }
            }
        }
        return powerupTarget;
    }

    public SubjectiveRobot? GetBallCarrier(RobotControls controls)
    {
        SubjectiveRobot? ballCarrier = null;

        if(controls.archiveRobots.Count > 1)
        {
            foreach (SubjectiveRobot robot in controls.archiveRobots)
            {
                if (Mathf.Approximately(robot.currentPosition.x, controls.updateBall.x) && 
                Mathf.Approximately(robot.currentPosition.z, controls.updateBall.z))
                {
                    ballCarrier = robot;
                }
            }
        }
        return ballCarrier.Value;
    }

    public Vector3 RecallPowerPositions(RobotControls controls)
    {
        if (powerupPos1 != Vector3.zero)
        {
            return powerupPos1;
        }
        else if (powerupPos2 != Vector3.zero)
        {
            return powerupPos2;
        }
        else if (powerupPos3 != Vector3.zero)
        {
            return powerupPos3;
        }
        else if (powerupPos4 != Vector3.zero)
        {
            return powerupPos4;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 TargetTragectoryPredictionSystem(RobotControls controls, SubjectiveRobot target)
    {
        float tTimeElapsed = 0;
        float pSpeed = 10;
        float tSpeed = 0;
        float pRatio = 0;
        float tRatio = 0;
        float tPosX = 0;

        Vector3 tPosA = Vector3.zero;
        Vector3 tPosB = Vector3.zero;
        Vector3 tPos = Vector3.zero;
        Vector3 tDistanceTraveled = Vector3.zero;
        Vector3 tmDistance = Vector3.zero;
        
        while(tPos == Vector3.zero)
        {
            if (tTimeElapsed == 0)
            {
                tPosA = target.currentPosition;
                tTimeElapsed += Time.deltaTime;
            }
            else if (tTimeElapsed > 0 && tTimeElapsed < 1)
            {
                tTimeElapsed += Time.deltaTime;
            }
            else if (tTimeElapsed >= 1)
            {
                tTimeElapsed = 0;
                tPosB = target.currentPosition;
                tDistanceTraveled = tPosB - tPosA;
                tmDistance = tPosB - controls.myself.currentPosition;
                tSpeed = tDistanceTraveled.magnitude / tTimeElapsed;
                
                pRatio = pSpeed / tSpeed;
                tRatio = tSpeed / pSpeed;
                tPosX = ( - pRatio + Mathf.Sqrt(pRatio * pRatio - 4 * tRatio * tmDistance.magnitude )) / 2 * tRatio;
                tPos = new Vector3(tPosX, controls.myself.currentPosition.y, tPosX * pRatio);
            }
        }
        return tPos;
    }
}
