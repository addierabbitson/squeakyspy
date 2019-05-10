using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform[] nodes;
    public GameObject player;
    private int destPoint = 0;
    private NavMeshAgent navAgent;
    public bool setNextDestinationNow = true;


    public void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = true;
        NextNode();
    }


    public void NextNode() {
        if (nodes.Length == 0) {
            return;
        }
        
        navAgent.destination = nodes[destPoint].position;
        destPoint = (destPoint + 1) % nodes.Length;

        setNextDestinationNow = true;
    }

    public void MoveToPlayer() {
        navAgent.destination = player.transform.position;
        Invoke("NextNode", 4f);
    }


    public void Update() {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.2f && setNextDestinationNow) {
            Invoke("NextNode", 3f);
            setNextDestinationNow = false;
        }
    }

    public void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            setNextDestinationNow = false;
            MoveToPlayer();
        }
    }
}

