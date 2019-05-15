using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public float squeak;
    private Scene currentScene;
    private string sceneName;
    public GameObject squeakMeter;
    public string timer;
    public float volume;

    private bool isLevel1;
    private bool isLevel2;

    private void Start() {
        isLevel1 = false;
        isLevel2 = false;
        volume = 1.0f;
    }

    private void Update() {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (squeak > 100)
            squeak = 100;
        else if (squeak < 0)
            squeak = 0;

        AudioListener.volume = volume;

        if (sceneName == "Level 1" && !isLevel1) {
            Destroy(this.gameObject.GetComponent<AudioSource>());
            squeak = 0;
            isLevel1 = true;
            squeakMeter.SetActive(true);
            squeakMeter.SetActive(true);
            Time.timeScale = 1.0f;
        }
        else if (sceneName != "Level 1") {
            isLevel1 = false;
        }

        if (sceneName == "Level 2" && !isLevel2) {
            Destroy(this.gameObject.GetComponent<AudioSource>());
            squeak = 0;
            isLevel2 = true;
            squeakMeter.SetActive(true);
            Time.timeScale = 1.0f;
        }
        else if (sceneName != "Level 2") {
            isLevel2 = false;
        }
    }

    private void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
