using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour {

    public Text timerLabel;
    private float time;

    void Update() {
        time += Time.deltaTime;
   
        var minutes = time / 60;
        var seconds = time % 60;
        var fraction = (time * 100) % 100;

        timerLabel.text = string.Format("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
        GameController.Instance.timer = timerLabel.text.ToString();
    }
}
