using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform[] nodes;
    private int destPoint = 0;
    private NavMeshAgent navAgent;
    private bool setNextDestinationNow = true;


    void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = true;
        NextNode();
    }


    void NextNode() {
        if (nodes.Length == 0) {
            return;
        }
        
        navAgent.destination = nodes[destPoint].position;
        destPoint = (destPoint + 1) % nodes.Length;

        Debug.Log(destPoint);

        setNextDestinationNow = true;

    }


    void Update() {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.2f && setNextDestinationNow)
        {
            Invoke("NextNode", 3f);
            setNextDestinationNow = false;
        }
    }
}

