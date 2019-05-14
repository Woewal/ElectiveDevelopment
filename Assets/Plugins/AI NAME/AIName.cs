using AI;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu]
public class AIName : Brain
{
    public override void Initialize(RobotControls controls)
    {
        Debug.Log($"AIRando awoke at {controls.me.position}. Ready to rumble.");
        controls.goTo(new Vector3(10, 0, 20));
    }

    public override void UpdateControls(RobotControls controls)
    {
        Debug.Log($"I can see {controls.visibleTargets.Length} targets.");
    }

    public override void OnTargetReached(RobotControls controls)
    {
        Debug.Log($"AIRando reached {controls.me.position}.");
    }
}
