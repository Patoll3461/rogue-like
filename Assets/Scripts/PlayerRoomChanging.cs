using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRoomChanging : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Transition")) {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton5)) {
                collision.gameObject.GetComponent<RoomChanger>().ChangeRoom();
                transform.position = Vector3.zero;
            }
        }
    }
}
