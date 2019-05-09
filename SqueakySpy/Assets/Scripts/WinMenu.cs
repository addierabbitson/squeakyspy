using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {

    private AudioClip mSoundClip;
    private AudioSource mAudioSource;

    private void Start() {
        mSoundClip = GetComponent<AudioSource>().clip;
        mAudioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mAudioSource.PlayOneShot(mSoundClip);
        }
        Time.timeScale = 0.0f;
    }
}
