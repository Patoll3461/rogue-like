using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int health;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color normalColor;
    private float knockbackForce = 30f;
    private float knockbackDuration = 0.09f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = 2;
        normalColor = GetComponent<SpriteRenderer>().color;
        var agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.SetDestination(player.transform.position);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Sword")) {
            if (collision.gameObject.transform.parent.GetComponent<PlayerAttack>().isAttacking) {
                health--;
                if (health <= 0) {
                    Destroy(GetComponent<CircleCollider2D>(), 0f);
                }
                StartCoroutine(KnockbackCoroutine((transform.position - collision.transform.position).normalized));
            }
        }
    }

    void ResetColor() {
        GetComponent<SpriteRenderer>().color = normalColor;
    }

        private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        float timer = 0;
        while (timer < knockbackDuration)
        {
            rb.velocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        if (health <= 0) {
            Destroy(gameObject, 0f);
        }
    }
}
