using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public float squeak;

    private void Update() {
        if (squeak > 100)
            squeak = 100;
        else if (squeak < 0)
            squeak = 0;
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
