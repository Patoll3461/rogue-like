using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        } else {
            Destroy(gameObject); // Prevent duplicate AudioManager instances
        }
    }
}
