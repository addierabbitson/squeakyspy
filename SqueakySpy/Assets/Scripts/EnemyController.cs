using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform[] nodes;
    public GameObject player;
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

        setNextDestinationNow = true;
    }

    void MoveToPlayer() {
        navAgent.destination = player.transform.position;
        Invoke("NextNode", 4f);
    }


    void Update() {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.2f && setNextDestinationNow) {
            Invoke("NextNode", 3f);
            setNextDestinationNow = false;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            setNextDestinationNow = false;
            MoveToPlayer();
        }
    }
}

