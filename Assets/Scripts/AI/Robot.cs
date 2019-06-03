using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class Robot : MonoBehaviour
    {
        //Visual management
        VisualsManager visuals;

        public Brain brain;
        public RobotControls controls;
        public PlayerAttack playerAttack;
        public PlayerMovement playerMovement;
		public Health health;

        public float speed;
        public int damage;
        public int team;
        public bool alive;
        public int id;
		public Vector3 respawnLocation;

		public bool IsAlive { get
			{
				return health.CurrentHP > 0;
			}
		}

		void Awake()
		{
			health = GetComponent<Health>();	
		}

		public void Start()
        {
            visuals = GetComponent<VisualsManager>();
            respawnLocation = transform.position;
            RobotManager.Instance.Register(this);
            RobotManager.Instance.OnRobotAdded += (robot) =>
            {
                if(robot != this)
                {
                    controls.archiveRobots.Add(new SubjectiveRobot()
                    {
                        id = robot.id,
                        team = robot.team,
                    });
                }
            };

            name = $"Robot: {id}";

            controls.archiveRobots = new List<SubjectiveRobot>();
            InitialDataFill();

            controls.goTo = GoTo;
            controls.attack = Attack;
            controls.passBall = PassBall;
        }

        void Update()
        {
            UpdateSelf();
            UpdateData();
            UpdateBall();

            brain.UpdateAttack(controls);
            brain.UpdateMovement(controls);
            brain.UpdateBallPass(controls);
        }

        //Fills in data for the first time with data that will be constant throughout the game (id,team)
        private void InitialDataFill()
        {
            var allRobots = RobotManager.Instance.allRobots;
            foreach (var robot in allRobots)
            {
                if (robot == this) continue;
                controls.archiveRobots.Add(new SubjectiveRobot()
                {
                    id = robot.id,
                    team = robot.team,
                });
            }

        }

        /*
         Updates data for each existing robot 
         Data updates based on whether this robot is seen
         Refreshed data is: position, health amount, is alive (dead or not), last shoot direction,is seen,
         */
        private void UpdateData()
        {
            for (int i = 0; i < controls.archiveRobots.Count; i++)
            {
                var information = controls.archiveRobots[i];
                var robot = RobotManager.Instance.allRobots.Where(x => x.id == information.id).First();
                if (CanSee(robot.gameObject))
                {
                    information.currentHealth = robot.health.CurrentHP;
                    information.currentPosition = robot.playerMovement.currentRobotPosition;
                    information.isAlive = robot.alive;
                    information.lastShootDir = robot.playerAttack.lastShootDirection;
                    information.isSeen = true;
                }
                else
                {
                    information.isSeen = false;
                }
                controls.archiveRobots[i] = information;
            }
        }

        //Monitors data about the robot itself
        private void UpdateSelf()
        {
            controls.myself = RobotToSubjectiveRobot(this, true, true);
        }

        //Updates Ball position
        private void UpdateBall()
        {
            controls.updateBall = BallManager.Instance.ballTransform.position;
        }

        //Transforms robot class into a struct of data
        public SubjectiveRobot RobotToSubjectiveRobot(Robot robot, bool canSee, bool isTeammate)
        {
            if (canSee || isTeammate)
            {
                return new SubjectiveRobot
                {
                    currentPosition = robot.playerMovement.currentRobotPosition,
                    currentHealth = robot.health.CurrentHP,
                    lastShootDir = robot.playerAttack.lastShootDirection,
                    isAlive = robot.alive,
                    isSeen = canSee,
                    team = robot.team,
                    id = robot.id
                };
            
            }
            else
            {
                return new SubjectiveRobot
                {
                    isSeen = canSee,
                    team = robot.team,
                    id = robot.id
                };
            }
        }

        //Checks is certain robot is seen or not
        private bool CanSee(GameObject target)
        {
            RaycastHit hit;

            var relativePosition = (target.transform.position + Vector3.up * 0.5f) - (transform.position + Vector3.up * 0.5f);

            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, relativePosition, out hit))
            {
                if (hit.transform.gameObject == target)
                {
                    return true;
                }

                return false;
 
            }
            else
                return false;
        }

    #region Actions with robot

        public void DealDamage(int dmg)
        {
            health.CurrentHP -= dmg;
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
            playerMovement.MoveTowards(_position);
        }

    #endregion

        /*private void OnDrawGizmos()
        {
            foreach (var robot in RobotManager.Instance.allRobots)
            {
                Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, robot.transform.position + Vector3.up * 0.5f);
            }
            
        }*/



    #region Obsolete Code

        /*private bool AlreadyRegistered(Robot robot)
        {
            bool newRobot = true;
            // int identicalPos;
            for (int i = 0; i < controls.otherRobots.Count(); i++)
            {
                if (robot.id == controls.otherRobots[i].id)
                {
                    newRobot = false;
                    controls.otherRobots[i] = RobotToSubjectiveRobot(robot, CanSee(robot), IsTeammate(robot));
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
        }*/

        /*private bool IsTeammate(Robot target)
        {
            if (team == target.team)
                return true;          
            else
                return false;
        }*/

        /// <summary>
        /// Maybe this should not be checked every frame, for performance.
        /// Raycasting is expensive!
        /// Creating arrays creates garbage! 
        /// </summary>
        /*private void UpdateTargets()
        {
            // This list should be stored somewhere central for better performance! Finding objects is expensive!
            // Take the robots that are alive and that we can see and create a target of them
            //controls.visibleTargets = allRobots.Where(r => r.IsAlive).Where(CanSee).Select(RobotToSubjectiveRobot).ToArray();
            // controls.otherRobots = allRobots.Where(r => r.AlreadyRegistered(r)).Select(r => r.RobotToSubjectiveRobot(r, CanSee(r), IsTeammate(r))).ToArray();
            // controls.archiveRobots = allRobots.Where(r => r.IsTeammate(r)).Where(r => r.CannotSee(r)).Select(r => r.RobotToSubjectiveRobot(r)).ToList();
            
            var allRobots = RobotManager.Instance.allRobots;
            var allPickups = PickupManager.Instance.allPickups;
            controls.updateBall = BallManager.Instance.ballTransform.position;
            controls.updateRobots = allRobots.Select(r => r.RobotToSubjectiveRobot(r,true,true)).ToList();
            controls.updatePickup = allPickups.Where(p => p.CanSee(this.gameObject)).Select(p => p.PickupToSubjectivePickup(p)).ToList();
        }*/

        /*public void ArchiveUpdate()
        {
            foreach (SubjectiveRobot robot in controls.updateRobots)
            {
                if (robot.id != this.id)
                {

                    if (controls.archiveRobots.Any())
                    {
                        var archivedRobots = controls.archiveRobots.Where(x => x.id == robot.id);
                        if (archivedRobots.Any())
                        {
                            var archivedRobot = archivedRobots.First();
                            if (controls.archiveRobots.Contains(archivedRobot))
                            {
                                int archivedRobotIndex = controls.archiveRobots.IndexOf(archivedRobot);
                                Vector3 currentPositionVar = controls.archiveRobots[archivedRobotIndex].currentPosition;
                                Vector3 lastShootDirVar = controls.archiveRobots[archivedRobotIndex].lastShootDir;
                                if (robot.currentPosition != null)
                                {
                                    currentPositionVar = robot.currentPosition;
                                }
                                if (robot.lastShootDir != null)
                                {
                                    lastShootDirVar = robot.lastShootDir;
                                }
                                controls.archiveRobots[archivedRobotIndex] = new SubjectiveRobot
                                {
                                    currentPosition = currentPositionVar,
                                    currentHealth = robot.currentHealth,
                                    lastShootDir = lastShootDirVar,
                                    isAlive = robot.isAlive,
                                };
                            }
                            else
                            {
                                controls.archiveRobots.Add(new SubjectiveRobot
                                {
                                    currentPosition = robot.currentPosition,
                                    currentHealth = robot.currentHealth,
                                    lastShootDir = robot.lastShootDir,
                                    isAlive = robot.isAlive,
                                    team = robot.team,
                                    id = robot.id
                                });
                            }
                        }

                    }
                    else
                    {
                        controls.archiveRobots.Add(new SubjectiveRobot
                        {
                            currentPosition = robot.currentPosition,
                            currentHealth = robot.currentHealth,
                            lastShootDir = robot.lastShootDir,
                            isAlive = robot.isAlive,
                            team = robot.team,
                            id = robot.id
                        });
                    }
                }
            }
        }*/
        
        /*public SubjectiveRobot RobotToSubjectiveRobot(Robot robot)
        {
            return new SubjectiveRobot
            {
                currentPosition = robot.playerMovement.currentRobotPosition,
                currentHealth = robot.health,
                lastShootDir = robot.playerAttack.lastShootDirection,
                isAlive = robot.alive,
                team = robot.team,
                id = robot.id
            };
        }*/
        
    #endregion

    }
}