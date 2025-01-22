using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class HealthCarry : MonoBehaviour
{
    public float PlayerHealth;
    public bool ControllerMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float health, bool mode) {
        PlayerHealth = health;
        ControllerMode = mode;
    }
}
