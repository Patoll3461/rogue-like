using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color normalColor;
    private float knockbackForce = 30f;
    private float knockbackDuration = 0.09f;
    private Rigidbody2D rb;
    [SerializeField] private HealthBar healthBarInstance;
    [SerializeField] private HealthBarManager healthBarManager;
    private bool invincible;
    // Start is called before the first frame update
    void Awake()
    {
        invincible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = 3f;
        currentHealth = maxHealth;
        normalColor = GetComponent<SpriteRenderer>().color;
        var agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.SetDestination(player.transform.position);
        rb = GetComponent<Rigidbody2D>();
        healthBarManager = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthBarManager>();
        healthBarInstance = healthBarManager.CreateHealthBar(transform);
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);

        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        UpdateHealthBar();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Sword") && !invincible) {
            if (collision.gameObject.transform.parent.GetComponent<PlayerAttack>().isAttacking) {
                currentHealth--;
                invincible = true;
                UpdateHealthBar();
                if (currentHealth <= 0) {
                    GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().enemyCount--;
                    if (GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().enemyCount <= 0) {
                        GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().ShowDoor();
                    }
                    Destroy(GetComponent<CircleCollider2D>(), 0f);
                }
                Invoke("ResetColor", 0.2f);
                StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized));
            }
        }

        if (collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<PlayerHealth>().Damage(GetComponent<Collider2D>(), 0.1f);
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

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Hazard") && GameObject.FindGameObjectWithTag("Spike").GetComponent<TileAnimationTrigger>().extended) {
            if (!invincible) {
                currentHealth--;
                invincible = true;
                UpdateHealthBar();
                if (currentHealth <= 0) {
                    GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().enemyCount--;
                    if (GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().enemyCount <= 0) {
                        GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().ShowDoor();
                    }
                    Destroy(GetComponent<CircleCollider2D>(), 0f);
                }
                Invoke("ResetColor", 0.8f);
                StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized));
            } 
        }
    }
}
