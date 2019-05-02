using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDetection : MonoBehaviour {

    public GameObject progressionBar;
    public float health;


    private void Update() {
        progressionBar.GetComponent<Image>().fillAmount = health * 0.01f;
    }
}
