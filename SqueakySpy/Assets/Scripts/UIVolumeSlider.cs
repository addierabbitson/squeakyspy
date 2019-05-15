using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVolumeSlider : MonoBehaviour {

    public GameObject meme1;
    public GameObject meme2;

    public void SetVolume(float vol) {
        GameController.Instance.volume = vol;
    }

    private void Update() {
        if (GameController.Instance.volume > 0f) {
            meme1.SetActive(true);
            meme2.SetActive(false);
        }
        else {
            meme1.SetActive(false);
            meme2.SetActive(true);
        }
    }
}
