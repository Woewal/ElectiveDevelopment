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

    public struct RobotControls
    {
        #region Actions

        public Action<Vector3> goTo;
        public Action<Vector3> attack;
        public Action<Vector3> passBall;

        #endregion

        #region Data

        public Target me;
        // public Team team;

        public Target[] visibleTargets;

        #endregion
    }

    public abstract class Brain : ScriptableObject
    {
        public abstract void Initialize(RobotControls controls);
        public abstract void UpdateControls(RobotControls controls);
        public abstract void OnTargetReached(RobotControls controls);
    }
}
