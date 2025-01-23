using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Boolean controllerMode = false;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(gameObject);
        controllerMode = false;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click() {
        SceneManager.LoadScene(1);
    }

    public void ChangeMode() {
        controllerMode = !controllerMode;
        if (controllerMode) {
            text.text = "Controller Mode: On";
        } else {
            text.text = "Controller Mode: Off";
        }
    }

}

