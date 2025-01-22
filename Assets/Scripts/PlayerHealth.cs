using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool invincible;

    private float knockbackForce = 10f;
    private float knockbackDuration = 0.07f;
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

    public void Damage(Collider2D collision) {
        if (!invincible) {
            currentHealth--;
            invincible = true;
            if (currentHealth <= 0) {
                Destroy(healthBarInstance.gameObject);
                Destroy(this.gameObject);
            }
            Invoke("ResetColor", 0.1f);
            StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized));
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
            Destroy(gameObject, 0f);
        }
    }

    private void UpdateHealthBar()
    {
        healthBarInstance.SetHealth(currentHealth / maxHealth);
    }
}
