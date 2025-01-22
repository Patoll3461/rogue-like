using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<HealthCarry>().UpdateHealth(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth, GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().controllerMode);
        SceneManager.LoadScene(room + 1);
    }

    public void ShowDoor() {
        GameObject currDoor = Instantiate(door, transform.position, Quaternion.identity);
        currDoor.GetComponent<RoomChanger>().room = room;
        for (int i = 1; i < GameObject.FindGameObjectsWithTag("Transition").Length - 1; i++){
            Destroy(GameObject.FindGameObjectsWithTag("Transition")[i]);
        }
    }
}
