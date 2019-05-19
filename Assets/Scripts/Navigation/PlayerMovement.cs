using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform testDestination;

    private Vector3 lastDestination;
    private NavMeshAgent navMeshAgent;

    private Transform robotTransform;
    public Vector3 currentRobotPosition { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        robotTransform = this.GetComponent<Transform>();
    }

    public void SetDestination(Vector3 _target)
    {
        lastDestination = _target;
        navMeshAgent.SetDestination(lastDestination);
    }
    // Update is called once per frame
    void Update()
    {
        currentRobotPosition = robotTransform.position;
        //this is a testing input update, it needs to be deleted later;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetDestination(testDestination.position);
        }
    }
}
