using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeOpen : MonoBehaviour {

    public GameObject safeOpen;
    public GameObject safeClosed;
    public GameObject winMenu;
    public GameObject squeakMeter;
    public GameObject crossHair;
    public GameObject pressE;
    private bool isCasting;

    void OnGUI() {
        if (isCasting) {
            pressE.SetActive(true);
        }
        else {
            pressE.SetActive(false);
        }
    }

    void Update() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanceOfRay = 5;

        if (Physics.Raycast(ray, out hit, distanceOfRay)) {
            if (hit.collider.tag.Equals("Safe")) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    safeClosed.SetActive(false);
                    safeOpen.SetActive(true);
                    winMenu.SetActive(true);
                    squeakMeter.SetActive(false);
                    crossHair.SetActive(false);
                    pressE.SetActive(false);
                }
                isCasting = true;
            }
            else
            {
                isCasting = false;
            }
        }
    }
}
