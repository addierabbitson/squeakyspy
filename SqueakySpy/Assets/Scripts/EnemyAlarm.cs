using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarm : MonoBehaviour {

    public GameObject loseMenu;
    public GameObject squeakMeter;
    public GameObject crossHair;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            loseMenu.SetActive(true);
            squeakMeter.SetActive(false);
            crossHair.SetActive(false);
            Time.timeScale = 0.0f;
        }
    }
}
