using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Finish")){
            Destroy(this.gameObject);
        }
    }
}
