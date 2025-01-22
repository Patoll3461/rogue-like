using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Hazard")) {
            if (GameObject.FindGameObjectWithTag("Spike").GetComponent<TileAnimationTrigger>().extended) {
                transform.parent.GetComponent<PlayerHealth>().Damage(collision, 0.8f);
            }
        }
    }
}

