using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && GameObject.FindGameObjectWithTag("Spike").GetComponent<TileAnimationTrigger>().extended) {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(collision, 0.8f);
        }
    }

}

