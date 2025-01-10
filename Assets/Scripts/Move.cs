using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 6;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(new Vector3(1, 1, 0));
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
    }
}
