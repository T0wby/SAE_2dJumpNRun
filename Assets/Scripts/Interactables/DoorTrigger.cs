using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Lever _lever;
    [SerializeField] private SO_LevelObjects _levelObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _lever.LeverPulled)
        {
            _door.DoorOpen = true;
            _door.OpenDoor();
            SceneManager.LoadScene("LevelTwo", LoadSceneMode.Additive);
            // Adding scene as string to SO incase we save
            _levelObjects.activeScenes[1] = "LevelTwo";
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            _door.DoorText.SetActive(true);
        }
    }
}
