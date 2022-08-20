using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private SO_LevelObjects _levelObjects;
    [SerializeField] private TMP_Text _collectedDiamonds;
    [SerializeField] private TMP_Text _maxDiamonds;

    void Awake()
    {
        _maxDiamonds.text = _levelObjects.maxCountDiamonds.Length.ToString();
        _collectedDiamonds.text = _levelObjects.diamondCount.ToString();
    }

    public void BackToMainMenuScene()
    {
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.PauseGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
