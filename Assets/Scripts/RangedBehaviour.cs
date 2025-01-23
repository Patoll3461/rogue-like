using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RangedBehaviour : MonoBehaviour
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
    [SerializeField] private AudioClip death;

    [SerializeField] private float safeDistance = 6f; // Minimum distance from the player
    [SerializeField] private float retreatDistance = 6f; // Maximum distance to retreat
    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    void Awake()
    {
        invincible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = 2f;
        currentHealth = maxHealth;
        normalColor = GetComponent<SpriteRenderer>().color;
        var agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        healthBarManager = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthBarManager>();
        healthBarInstance = healthBarManager.CreateHealthBar(transform);
        safeDistance = 6f;
        retreatDistance = 6f;
        UpdateHealthBar();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = (transform.position - player.transform.position).normalized;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If too close, move away from the player
        if (distanceToPlayer < safeDistance)
        {
            Vector3 retreatPosition = player.transform.position + directionToPlayer * retreatDistance;
            GetComponent<NavMeshAgent>().SetDestination(retreatPosition);
        }
        else
        {
            // Stay within range and circle the player
            Vector3 circlePosition = player.transform.position + directionToPlayer * safeDistance;
            GetComponent<NavMeshAgent>().SetDestination(circlePosition);
        }

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
                if (currentHealth <= 1) {
                    GetComponent<AudioSource>().clip = death;
                } else {
                    GetComponent<AudioSource>().Play();
                }
                StopAllCoroutines();
                StartCoroutine(Shoot());
                currentHealth--;
                invincible = true;
                UpdateHealthBar();
                if (currentHealth <= 0) {
                    GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().enemyCount--;
                    Destroy(GetComponent<BoxCollider2D>(), 0f);
                }
                Invoke("ResetColor", 0.2f);
                StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized));
            }
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
            GetComponent<AudioSource>().Play();
            Destroy(GetComponent<SpriteRenderer>());
            if (GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().enemyCount <= 0 && GameObject.FindGameObjectWithTag("Transition") == null) {
                GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().ShowDoor();
            }
            if (GetComponent<AudioSource>().isPlaying) {
                Invoke("Kill", 0.5f);
            } else {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthBarInstance.SetHealth(currentHealth / maxHealth);
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Hazard") && GameObject.FindGameObjectWithTag("Spike").GetComponent<TileAnimationTrigger>().extended) {
            if (!invincible) {
                if (currentHealth <= 1) {
                    GetComponent<AudioSource>().clip = death;
                } else {
                    GetComponent<AudioSource>().Play();
                }
                currentHealth--;
                invincible = true;
                UpdateHealthBar();
                if (currentHealth <= 0) {
                    GameObject.FindGameObjectWithTag("Room").GetComponent<RoomChanger>().enemyCount--;
                    Destroy(GetComponent<CircleCollider2D>(), 0f);
                }
                Invoke("ResetColor", 0.8f);
                StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized));
            } 
        }
    }

    private void Kill() {
        if (GetComponent<AudioSource>().isPlaying) {
            Invoke("Kill", 0.5f);
        } else {
            Destroy(gameObject);
        }
    }

    private IEnumerator Shoot() {
        while(true) {
            yield return new WaitForSeconds(3f);
            Vector3 targ = player.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;        

            GameObject bullet1 = Instantiate(bullet, transform.position + transform.right, Quaternion.Euler(new Vector3(0, 0, angle)));
            bullet1.GetComponent<Rigidbody2D>().AddForce(transform.right * 1000, ForceMode2D.Force);
        }
    }
}
