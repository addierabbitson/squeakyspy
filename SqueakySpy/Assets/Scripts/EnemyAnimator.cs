using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    private Animator animator;
    private EnemyController enemyController;

    void Start() {
        animator = GetComponent<Animator>();
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    void Update() {
        if (enemyController.setNextDestinationNow == false) {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
        else if (enemyController.setNextDestinationNow == true) {

            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
        }
    }
}
