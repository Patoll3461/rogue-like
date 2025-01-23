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
    [SerializeField] private GameObject heal;
    [SerializeField] private AudioClip ascend;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom() {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = ascend;
        GetComponent<AudioSource>().Play();
        Invoke("Switch", 3f);
    }

    public void ShowDoor() {
        if (room == 5 || room == 10 || room == 13) {
            Instantiate(heal, new Vector3(3, 0, 0), Quaternion.identity);
        }
        GameObject currDoor = Instantiate(door, transform.position, Quaternion.identity);
        currDoor.GetComponent<RoomChanger>().room = room;
        for (int i = 1; i < GameObject.FindGameObjectsWithTag("Transition").Length - 1; i++){
            Destroy(GameObject.FindGameObjectsWithTag("Transition")[i]);
        }
    }

    public void Switch() {
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<HealthCarry>().UpdateHealth(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth, GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().controllerMode);
        if (room == 15) {
            Destroy(GameObject.FindGameObjectWithTag("Respawn"));
            Destroy(GameObject.FindGameObjectWithTag("GameController"));
        }
        SceneManager.LoadScene(room + 2);
    }
}
