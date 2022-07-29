using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door _Door;
    [SerializeField] private Lever _Lever;
    [SerializeField] private SO_LevelObjects _LevelObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Player entered");

        if (collision.gameObject.CompareTag("Player") && _Lever.LeverPulled)
        {
            _Door.DoorOpen = true;
            _Door.OpenDoor();
            SceneManager.LoadScene("LevelTwo", LoadSceneMode.Additive);
            _LevelObjects.activeScenes[1] = "LevelTwo";
        }
    }
}
