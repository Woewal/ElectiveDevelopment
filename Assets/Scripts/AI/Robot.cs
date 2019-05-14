using System.Linq;
using UnityEngine;

namespace AI
{
    public class Robot : MonoBehaviour
    {
        public Brain brain;
        public RobotControls controls;
        public PlayerAttack playerAttack;
        public PlayerMovement playerMovement;

        public int health;
        public float speed;
        public int damage;

        public bool IsAlive { get; private set; }

        public void Start()
        {
            IsAlive = true;

            // Set up the controls - what doesn't change?

            //controls.team = Team.blue;
            controls.goTo = GoTo;
            controls.attack = Attack;
            controls.passBall = PassBall;

            UpdateData();
            // Initialize brain
            brain.Initialize(controls);
        }

        #region Refreshing brain

        public void Update()
        {
            // Update controls and brain
            UpdateData();
            UpdateTargets();
            brain.UpdateControls(controls);
        }

        private void UpdateData()
        {
            controls.me = GetAsTarget(this);
        }

        /// <summary>
        /// Maybe this should not be checked every frame, for performance.
        /// Raycasting is expensive!
        /// Creating arrays creates garbage! 
        /// </summary>
        private void UpdateTargets()
        {
            // This list should be stored somewhere central for better performance! Finding objects is expensive!
            var allRobots = FindObjectsOfType<Robot>();
            // Take the robots that are alive and that we can see and create a target of them
            controls.visibleTargets = allRobots.Where(r => r.IsAlive).Where(CanSee).Select(GetAsTarget).ToArray();
        }

        public static Target GetAsTarget(Robot robot)
        {
            return new Target
            {
                position = robot.transform.position,
                rotation = robot.transform.rotation,
                alive = robot.IsAlive
            };
        }

        private bool CanSee(Robot target)
        {
            // Do some raycasting
            return true;
        }

        #endregion

        #region Control methods

        void PassBall(Vector3 _target)
        {
            
        }

        void Attack(Vector3 _target)
        {
            playerAttack.Shoot(_target);
        }

        private void GoTo(Vector3 _position)
        {
            playerMovement.SetDestination(_position);
        }

        #endregion
    }
}