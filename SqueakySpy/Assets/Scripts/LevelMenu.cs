using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

    private AudioClip mSoundClip;
    private AudioSource mAudioSource;

    private void Start() {
        mSoundClip = GetComponent<AudioSource>().clip;
        mAudioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            mAudioSource.PlayOneShot(mSoundClip);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            mAudioSource.PlayOneShot(mSoundClip);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            mAudioSource.PlayOneShot(mSoundClip);
        }
        Time.timeScale = 1.0f;
    }
}
