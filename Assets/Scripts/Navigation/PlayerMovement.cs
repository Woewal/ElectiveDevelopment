using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform testDestination;

    private Vector3 lastDestination;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 _target)
    {
        lastDestination = _target;
        navMeshAgent.SetDestination(lastDestination);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetDestination(testDestination.position);
        }
    }
}
