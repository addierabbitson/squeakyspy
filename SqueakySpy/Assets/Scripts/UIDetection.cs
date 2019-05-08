using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDetection : MonoBehaviour {

    public GameObject progressionBar;
    public float squeak;
    public float speed;

    private void Update() {
        squeak = GameController.Instance.squeak;
        progressionBar.GetComponent<Image>().fillAmount = Mathf.Lerp(progressionBar.GetComponent<Image>().fillAmount, squeak * 0.01f, Time.deltaTime * speed);
    }
}
