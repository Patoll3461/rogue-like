using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    public bool controllerMode;

    void Start()
    {
        speed = 6f;
        rotateSpeed = 210f;
        controllerMode = GameObject.FindGameObjectWithTag("Respawn").GetComponent<HealthCarry>().ControllerMode;
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (!controllerMode) {
            transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

            transform.Rotate(new Vector3(0, 0, -horizontalInput * Time.deltaTime * rotateSpeed));
        } else {
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            // Rotate the object to face the direction of movement
            if (movement != Vector3.zero) {
                // Calculate the rotation angle based on the movement direction
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            controllerMode = !controllerMode;
        }
    }
}
