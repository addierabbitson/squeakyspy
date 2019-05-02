using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform firstNode;
    private Transform firstPos;
    private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        firstPos = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, firstNode.position) < 2) {
            navAgent.destination = firstPos.position;
        }
        if (Vector3.Distance(transform.position, firstPos.position) < 2)
        {
            navAgent.destination = firstNode.position;
        }
    }
}
