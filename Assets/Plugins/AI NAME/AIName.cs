using AI;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu]
public class AIName : Brain
{
    public override void UpdateAttack(RobotControls controls)
    {
        //Debug.Log($"AIRando awoke at {controls.me.position}. Ready to rumble.");
        //controls.goTo(new Vector3(10, 0, 20));
    }

    public override void UpdateMovement(RobotControls controls)
    {
       
    }

    public override void UpdateBallPass(RobotControls controls)
    {
        //Debug.Log($"AIRando reached {controls.me.position}.");
    }
}
