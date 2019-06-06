using AI;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

[CreateAssetMenu]
public class AIAntreas : Brain
{
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

        // controls.goTo()
    }

    public override void UpdateBallPass(RobotControls controls)
    {
        //Debug.Log($"AIRando reached {controls.me.position}.");
    }

    // public GameObject GetClosest()
    // {
    //     GameObject closest = null;
    //     float distance = Mathf.Infinity;
    //     Vector3 position = transform.position;
    //     if(EnemyManager.Instance.enemies.Count > 0)
    //     {
    //         foreach (GameObject enemy in EnemyManager.Instance.enemies)
    //         {
    //             Vector3 diff = enemy.transform.position - position;
    //             float curDistance = diff.sqrMagnitude;
    //             if (curDistance < distance && enemy.GetComponent<Enemy>().currentHealth > 0)
    //             {
    //                 closest = enemy;
    //                 distance = curDistance;
    //             }
    //         }
    //     }
    //     return closest;
    // }


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
            // Move towards the ball
            // Shoot at ball carrier
        // }

    #endregion
}
