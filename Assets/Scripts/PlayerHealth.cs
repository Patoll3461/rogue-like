using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool invincible;

    private float knockbackForce = 20f;
    private float knockbackDuration = 0.08f;
    private Rigidbody2D rb;
    [SerializeField] private HealthBar healthBarInstance;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15f;
        currentHealth = maxHealth;
        invincible = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    public void Damage(Collider2D collision, float resistanceTime) {
        if (!invincible) {
            currentHealth--;
            invincible = true;
            if (currentHealth <= 0) {
                Destroy(healthBarInstance.gameObject);
                Destroy(gameObject);
            }
            Invoke("ResetColor", resistanceTime);
            StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized));
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Hazard") && GameObject.FindGameObjectWithTag("Spike").GetComponent<TileAnimationTrigger>().extended) {
            Damage(collision, 0.8f);
        }
    }

    void ResetColor() {
        invincible = false;
    }


    private IEnumerator KnockbackCoroutine(Vector2 direction) {
        float timer = 0;
        while (timer < knockbackDuration)
        {
            rb.velocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        healthBarInstance.SetHealth(currentHealth / maxHealth);
    }
}
