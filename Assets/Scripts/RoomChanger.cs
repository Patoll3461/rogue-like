using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public int room;
    [SerializeField] private GameObject[] rooms;
    public int enemyCount;
    [SerializeField] private GameObject door;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom() {
        DestroyImmediate(GetComponent<BoxCollider2D>());
        Destroy(GameObject.FindGameObjectWithTag("Room"), 0f);
        Instantiate(rooms[room + 1], Vector3.zero, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void ShowDoor() {
        GameObject currDoor = Instantiate(door, transform.position, Quaternion.identity);
        currDoor.GetComponent<RoomChanger>().room = room;
    }
}
