using AI;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class AIMonica : Brain
{
    protected SubjectivePickup _closestPickup;
    protected SubjectivePickup _closestHealth;

    protected bool _noclosestPickup;
    protected bool _noclosestHealth;

    float Cooldown = 0.5f;
    float Cooldowntime = 0;


    public override void UpdateData(RobotControls controls)
    {
        UpdateClosestPickup(controls);
    }

    public override void UpdateAttack(RobotControls controls)
    {
        if (_noClosestEnemy == false)
        {
            Cooldowntime -= Time.deltaTime;
            if (Cooldowntime > Cooldown) return;
            var EnemyDistance = _closestEnemy.currentPosition - controls.myself.currentPosition;
            if (EnemyDistance.magnitude > 5) return;
            controls.attack(_closestEnemy.currentPosition);
            Cooldowntime = 0.5f;
        }
        else
        {
            return;
        }
    }


    public override void UpdateMovement(RobotControls controls)
    {
        if (controls.myself.currentHealth < 50 || _noclosestHealth == false)
        {
            controls.goTo(_closestHealth.currentPickupPosition);
        }
        else
        {
            if (controls.myself.currentPosition.x == controls.updateBall.x || controls.myself.currentPosition.y == controls.updateBall.y)
            {
                if (_noclosestPickup == false)
                {
                    controls.goTo(_closestPickup.currentPickupPosition);
                }
                else
                {
                    {
                        if (_noClosestTeammate == false)
                            controls.goTo(_closestTeammate.currentPosition);
                        else
                        {
                            controls.goTo(Vector3.forward * Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                controls.goTo(controls.updateBall);
            }
        }
    }

    public override void UpdateBallPass(RobotControls controls)
    {
        if (controls.myself.currentHealth <= 40 || _noClosestTeammate == false || controls.myself.currentPosition.x == controls.updateBall.x || controls.myself.currentPosition.y == controls.updateBall.y)
        {
            controls.passBall(_closestTeammate.currentPosition);
        }
    }


    protected void UpdateClosestPickup(RobotControls controls)
    {
        SubjectivePickup? closestPickup;
        SubjectivePickup? closestHealth;
        closestPickup = null;
        closestHealth = null;
        float distanceMin = 1000f;

        if (controls.updatePickup.Count > 0)
        {
            foreach (SubjectivePickup pickup in controls.updatePickup)
            {
                if (pickup.ofType == "Health")
                {
                    if (Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition) < distanceMin)
                    {
                        distanceMin = Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition);
                        closestHealth = pickup;
                    }
                }
                else
                {
                    if (Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition) < distanceMin)
                    {
                        distanceMin = Vector3.Distance(pickup.currentPickupPosition, controls.myself.currentPosition);
                        closestPickup = pickup;
                    }
                }

            }
        }

        if (closestHealth.HasValue)
        {
            _closestHealth = closestHealth.Value;
            _noclosestHealth = false;
        }
        else
        {
            _noclosestHealth = true;
        }

        if (closestPickup.HasValue)
        {
            _closestPickup = closestPickup.Value;
            _noclosestPickup = false;
        }
        else
        {
            _noclosestPickup = true;
        }
    }
}


  

   
       



 

