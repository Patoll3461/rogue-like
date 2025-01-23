using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    [SerializeField] private float maxHealth;
    public bool invincible;

    private float knockbackForce = 20f;
    private float knockbackDuration = 0.08f;
    private Rigidbody2D rb;
    [SerializeField] private HealthBar healthBarInstance;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15;
        currentHealth = GameObject.FindGameObjectWithTag("Respawn").GetComponent<HealthCarry>().PlayerHealth;
        invincible = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    public void Damage(Collider2D collision, float resistanceTime, bool isBullet = false) {
        if (!invincible) {
            GetComponent<AudioSource>().Play();
            currentHealth--;
            invincible = true;
            if (currentHealth <= 0) {
                Destroy(healthBarInstance.gameObject);
                Destroy(GameObject.FindGameObjectWithTag("Respawn"));
                transform.GetChild(2).GetComponent<AudioSource>().Play();
                Invoke("Reload", 8f);
            }
            Invoke("ResetColor", resistanceTime);
            StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized, isBullet));
        }
    }

    public void Reload() {
        SceneManager.LoadScene(0);
    }


    void ResetColor() {
        invincible = false;
    }


    private IEnumerator KnockbackCoroutine(Vector2 direction, bool isBullet) {
        if (!isBullet) {
            float timer = 0;
            while (timer < knockbackDuration)
            {
                rb.velocity = direction * knockbackForce;
                timer += Time.deltaTime;
                yield return null;
            }
            rb.velocity = Vector2.zero;
            if (currentHealth <= 0) {
                //Destroy(gameObject);
            }
        } else {
            float timer = 0;
            while (timer < knockbackDuration)
            {
                rb.velocity = -direction * knockbackForce;
                timer += Time.deltaTime;
                yield return null;
            }
            rb.velocity = Vector2.zero;
            if (currentHealth <= 0) {
                //Destroy(gameObject);
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthBarInstance.SetHealth(currentHealth / maxHealth);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton5)) {
            if (collision.gameObject.CompareTag("Heal")) {
                maxHealth = 15f;
                currentHealth = 15f;
                UpdateHealthBar();
                collision.GetComponent<AudioSource>().Play();
                Destroy(collision.GetComponent<SpriteRenderer>());
                Destroy(collision.GetComponent<BoxCollider2D>());
            }
        }
    }
}
