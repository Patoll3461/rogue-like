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
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private bool isDashing = false;
    private float dashTimeRemaining;
    private float dashCooldownRemaining;

    void Start()
    {
        dashCooldown = 0.8f;
        dashDuration = 0.1f;
        speed = 6f;
        rotateSpeed = 250f;
        dashSpeed = 20f;
        controllerMode = GameObject.FindGameObjectWithTag("GameController").GetComponent<StartGame>().controllerMode;
    }

    void FixedUpdate()
    {

        dashCooldownRemaining -= Time.fixedDeltaTime;

        if (dashCooldownRemaining <= 0) {
            dashCooldownRemaining = 0;
        }

        UpdateDashTimer();

        if (isDashing) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (!controllerMode) {
            transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

            transform.Rotate(new Vector3(0, 0, -horizontalInput * Time.fixedDeltaTime * rotateSpeed));
        } else {
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * speed * Time.fixedDeltaTime;
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
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton1)) && dashCooldownRemaining <= 0) {
            StartCoroutine(StartDash());
        }
    }

    private IEnumerator StartDash() {
        isDashing = true;
        dashTimeRemaining = dashDuration;
        dashCooldownRemaining = dashCooldown;
        gameObject.transform.GetChild(1).GetComponent<AudioSource>().Play();
        GetComponent<PlayerHealth>().invincible = true;

        while (dashTimeRemaining >= 0) {
            transform.Translate(Vector3.up * dashSpeed * Time.deltaTime, Space.Self);
            dashTimeRemaining -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        isDashing = false;
        GetComponent<PlayerHealth>().invincible = false;
    }

    private void UpdateDashTimer() {
        Debug.Log(1 - (dashCooldownRemaining / dashCooldown));
        GameObject.FindGameObjectWithTag("Dash").GetComponent<HealthBar>().SetHealth(1 - (dashCooldownRemaining / dashCooldown));
    }
}
