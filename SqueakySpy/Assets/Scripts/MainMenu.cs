using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private AudioClip mSoundClip;
    private AudioSource mAudioSource;

    private void Start() {
        mSoundClip = GetComponent<AudioSource>().clip;
        mAudioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            mAudioSource.PlayOneShot(mSoundClip);
        }
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            mAudioSource.PlayOneShot(mSoundClip);
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            mAudioSource.PlayOneShot(mSoundClip);
        }
        if (Input.GetKeyUp(KeyCode.Return)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        Time.timeScale = 1.0f;
    }
}
