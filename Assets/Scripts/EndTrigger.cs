using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.IsPaused = false;
            GameManager.Instance.PauseGame();
            SceneManager.LoadSceneAsync("EndScreen");
        }
    }
}
