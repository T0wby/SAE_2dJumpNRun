using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }

    [SerializeField] private SO_GameSettings _gameSettings;
    [SerializeField] private SO_LevelObjects _levelObjects;

    private string _saveGameSettings;
    private string _saveGameObjects;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    public void Save()
    {
        _saveGameSettings = JsonUtility.ToJson(_gameSettings, true);
        _saveGameObjects = JsonUtility.ToJson(_levelObjects, true);

        string filePath = $"{Application.dataPath}/savegamesettings.cgsav";
        string filePathTwo = $"{Application.dataPath}/savegameobjects.cgsav";

        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.Write(_saveGameSettings);
        }

        using (StreamWriter writer = File.CreateText(filePathTwo))
        {
            writer.Write(_saveGameSettings);
        }

    }

    public void Load()
    {
        
    }
}
