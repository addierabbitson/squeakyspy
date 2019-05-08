using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInner : MonoBehaviour {

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