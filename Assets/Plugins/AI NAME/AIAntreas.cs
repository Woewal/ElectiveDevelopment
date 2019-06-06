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

    public float cooldown = 1;

    public override void UpdateAttack(RobotControls controls)
    {
        //Debug.Log($"AIRando awoke at {controls.me.position}. Ready to rumble.");
        //controls.goTo(new Vector3(10, 0, 20));
    }

    public override void UpdateMovement(RobotControls controls)
    {
        // string test = $"Id: {controls.myself.id} I can see: ";

        // foreach(var robot in controls.archiveRobots)
        // {
        //     if(robot.isSeen)
        //         test += $"{robot.id} ";
        // }

        controls.goTo(destinationPoint);
    }

    public override void UpdateBallPass(RobotControls controls)
    {
        //Debug.Log($"AIRando reached {controls.me.position}.");
    }

    public GameObject GetClosest(RobotControls controls)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = controls.myself.currentPosition;
        if(controls.archiveRobots.Count > 1)
        {
            foreach (SubjectiveRobot robot in controls.archiveRobots)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && enemy.GetComponent<Enemy>().currentHealth > 0)
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    public GameObject GetFurthest(RobotControls controls)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = controls.myself.currentPosition;
        if(controls.archiveRobots.Count > 1)
        {
            foreach (SubjectiveRobot robot in controls.archiveRobots)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && enemy.GetComponent<Enemy>().currentHealth > 0)
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    public GameObject GetBallCarrier(RobotControls controls)
    {
        GameObject closest = null;
        if(controls.archiveRobots.Count > 1)
        {
            foreach (SubjectiveRobot robot in controls.archiveRobots)
            {
                if (Mathf.Approximately(robot.currentPosition.z, controls.updateBall))
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    public GameObject PredictionShooting(RobotControls controls)
    {

        tDistanceTraveled = tPosB - tPosA;
        tSpeed = tDistanceTraveled.magnitute / tTimeElapsed;
        pSpeed = 10;
        
        tPosX = 
    }

    #region Ball carrier

        // if(possessBall)
        // {
            #region Powerup Exists

                // if(powerupExists && hp > hpThreshold || !alliesSeen)
                // {
                    //Move towards closest powerup
                    //Shoot closest opponent
                // }

            #endregion

            #region Evade closest opponent

                // else if(!powerupExists && hp > hpThreshold || !alliesSeen)
                // {
                    //Move away from closest opponent
                    //Shoot closest opponent
                // }

            #endregion

            #region Pass the ball

                // else if(hp <= hpThreshold && alliesSeen)
                // {
                    //Pass the ball to furthest ally
                    //possessBall = false
                // }

            #endregion

            #region Coast clear

                // else if(hp > hpThreshold && !opponentsSeen)
                // {
                    //Stop moving
                // }

            #endregion
        // }

    #endregion
    
    #region Ball persuer

        // else if(!possessBall)
        // {
            
            #region Ally ball

                // if(hp <= hpThreshold && alliesSeen)
                // {
                    //Pass the ball to furthest ally
                    //possessBall = false
                // }

            #endregion

            #region Opponent ball

                // else if(controls.ballUpdate && !opponentsSeen)
                // {
                    //Stop moving
                // }

            #endregion

            // Move towards the ball
            // Shoot at ball carrier
        // }

    #endregion
}
