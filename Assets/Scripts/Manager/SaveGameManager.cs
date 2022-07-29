using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }

    [SerializeField] private SO_GameSettings _gameSettings;
    [SerializeField] private SO_LevelObjects _levelObjects;

    private string _saveGameSettings;
    private string _saveGameObjects;
    private string _filePath;
    private string _filePathTwo;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _filePath = $"{Application.dataPath}/savegamesettings.cgsav";
        _filePathTwo = $"{Application.dataPath}/savegameobjects.cgsav";
    }

    public void Save()
    {
        Debug.Log("SAVED GAME.....");
        _saveGameSettings = JsonUtility.ToJson(_gameSettings, true);
        _saveGameObjects = JsonUtility.ToJson(_levelObjects, true);

        

        using (StreamWriter writer = File.CreateText(_filePath))
        {
            writer.Write(_saveGameSettings);
        }

        using (StreamWriter writer = File.CreateText(_filePathTwo))
        {
            writer.Write(_saveGameObjects);
        }

    }

    private void LoadFromJSON()
    {
        if (!File.Exists(_filePath))
            return;

        string contentSettings = string.Empty;
        string contentObjects = string.Empty;

        using (StreamReader reader = File.OpenText(_filePath))
        {
            contentSettings = reader.ReadToEnd();
        }

        using (StreamReader reader = File.OpenText(_filePathTwo))
        {
            contentObjects = reader.ReadToEnd();
        }

        JsonUtility.FromJsonOverwrite(contentSettings, _gameSettings);
        JsonUtility.FromJsonOverwrite(contentObjects, _levelObjects);
    }

    public void Load()
    {
        LoadFromJSON();

        if (_levelObjects.activeScenes == null)
            return;
        for (int i = 0; i < _levelObjects.activeScenes.Length; i++)
        {
            switch (i)
            {
                case 0:
                    SceneManager.LoadSceneAsync(_levelObjects.activeScenes[i]);
                    break;
                case 1:
                    SceneManager.LoadScene(_levelObjects.activeScenes[i], LoadSceneMode.Additive);
                    break;
                default:
                    break;
            }
        }
        GameObject player = GameObject.Find("Player");

        if(player == null)
            return;

        player.transform.position = _levelObjects.player.position;
        player.transform.rotation = _levelObjects.player.rotation;
        player.GetComponent<PlayerHealth>().Health = _levelObjects.health;
        GameManager.Instance.DiamondCount = _levelObjects.diamondCount;
    }
}
