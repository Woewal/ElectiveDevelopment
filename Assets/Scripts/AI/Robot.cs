using System.Collections.Generic;
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
        public Team team;
        public bool alive;
        public int id;

        List<RobotListElement> listOfRobots = new List<RobotListElement>();

        public bool IsAlive { get; private set; }

        public void Start()
        {
            IsAlive = true;
            RobotManager.Instance.Register(this);

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
            controls.myself = GetAsTarget(this, true, true);
        }

        /// <summary>
        /// Maybe this should not be checked every frame, for performance.
        /// Raycasting is expensive!
        /// Creating arrays creates garbage! 
        /// </summary>
        private void UpdateTargets()
        {
            // This list should be stored somewhere central for better performance! Finding objects is expensive!
            var allRobots = RobotManager.Instance.allRobots;
            // Take the robots that are alive and that we can see and create a target of them
            //controls.visibleTargets = allRobots.Where(r => r.IsAlive).Where(CanSee).Select(GetAsTarget).ToArray();
            controls.otherRobots = allRobots.Where(r => r.AlreadyRegistered(r)).Select(r => r.GetAsTarget(r, CanSee(r), IsTeammate(r))).ToArray();
        }

        public SubjectiveRobot GetAsTarget(Robot robot, bool canSee, bool isTeammate)
        {
            if (canSee || isTeammate)
            {
                return new SubjectiveRobot
                {
                    currentPosition = robot.playerMovement.currentRobotPosition,
                    currentHealth = robot.health,
                    lastShootDir = robot.playerAttack.lastShootDirection,
                    isAlive = robot.alive,
                    isSeen = canSee,
                    team = robot.team,
                    name = robot.name,
                    id = robot.id
                };
            
            }
            else
            {
                return new SubjectiveRobot
                {
                    isSeen = canSee,
                    team = robot.team,
                    name = robot.name,
                    id = robot.id
                };
            }
        }

        private bool AlreadyRegistered(Robot robot)
        {
            bool newRobot = true;
            // int identicalPos;
            for (int i = 0; i < controls.otherRobots.Count(); i++)
            {
                if (robot.id == controls.otherRobots[i].id)
                {
                    newRobot = false;
                    controls.otherRobots[i] = GetAsTarget(robot, CanSee(robot), IsTeammate(robot));
                }
            }
            if(newRobot)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanSee(Robot target)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerMovement.currentRobotPosition, target.transform.position, out hit))
            {
                if (hit.transform.gameObject == target.gameObject)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private bool IsTeammate(Robot target)
        {
            if (team == target.team)
                return true;          
            else
                return false;
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