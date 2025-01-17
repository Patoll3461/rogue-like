using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    [SerializeField] private int room;
    [SerializeField] private GameObject[] rooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom() {
        Instantiate(rooms[room + 1], Vector3.zero, Quaternion.identity);
        Destroy(transform.parent.parent.gameObject, 0f);
    }
}
