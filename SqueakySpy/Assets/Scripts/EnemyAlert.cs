using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlert : MonoBehaviour {

	public float scale;
    private float memeX;
    private float memeY;
    private float memeZ;
    private BoxCollider enemyTrigger;

    private void Start() {
        enemyTrigger = GetComponent<BoxCollider>();
        memeX = 1.892371f;
        memeY = 2.602818f;
        memeZ = 2.871061f;
    }

    void Update() {
        enemyTrigger.size = new Vector3(memeX * scale, memeY, memeZ * scale);
        scale = GameController.Instance.squeak * 0.05f;

        if (scale < 1.0f)
            scale = 1.0f;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            GameController.Instance.squeak += 30;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            GameController.Instance.squeak -= 30;
        }
    }
}
