using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }
}
