using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarm : MonoBehaviour {

    public GameObject loseMenu;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            loseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
