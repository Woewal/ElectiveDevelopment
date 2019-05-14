using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Robot : MonoBehaviour
    {
        public Brain brain;
        public PlayerAttack playerAttack;
        public PlayerMovement playerMovement;

        public int health;
        public float speed;
        public int damage;

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        void Scan()
        {
            
        }

        void PassBall()
        {
            
        }

        void Attack(Vector3 _target)
        {
            // playerAttack.Shoot(_target);
        }

        #region Control methods

        private void GoTo(Vector3 position) { /* Pathfinding */ }
        private void TurnTowards(Vector3 obj) { /* Some rotation code */ }

        #endregion
    }
}

