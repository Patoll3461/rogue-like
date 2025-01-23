using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Boolean isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton3)) {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
            isAttacking = true;
            Invoke("ResetAttacking", 0.25f);
        }
    }

    void ResetAttacking() {
        isAttacking = false;
    }
}
