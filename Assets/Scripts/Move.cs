using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    void Start()
    {
        speed = 6f;
        rotateSpeed = 210f;
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        transform.Rotate(new Vector3(0, 0, -horizontalInput * Time.deltaTime * rotateSpeed));
    }
}
