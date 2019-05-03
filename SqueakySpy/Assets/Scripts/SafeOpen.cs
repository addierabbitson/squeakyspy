using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeOpen : MonoBehaviour {

    public GameObject safeOpen;
    public GameObject safeClosed;
    public GameObject winMenu;
    public GameObject squeakMeter;
    public GameObject crossHair;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distanceOfRay = 5;

            if (Physics.Raycast(ray, out hit, distanceOfRay)) {
                if (hit.collider.tag.Equals("Safe"))
                {
                    safeClosed.SetActive(false);
                    safeOpen.SetActive(true);
                    winMenu.SetActive(true);
                    squeakMeter.SetActive(false);
                    crossHair.SetActive(false);
                }
            }
        }
    }
}
