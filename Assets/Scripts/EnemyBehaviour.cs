using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private int health;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color normalColor;
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
                    Destroy(gameObject, 0f);
                }
            }
        }
    }

    void ResetColor() {
        GetComponent<SpriteRenderer>().color = normalColor;
    }
}
