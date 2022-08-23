using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Audio;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }

    [SerializeField] private SO_GameSettings _gameSettings;
    [SerializeField] private SO_LevelObjects _levelObjects;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private GameObject _player;
    [SerializeField] private Player _playerScript;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameObject _continueText;
    [SerializeField] private Lever _lever;
    [SerializeField] private Door _door;
    [SerializeField] private AudioMixer _volMixer;
    [SerializeField] private Slider _volSlider;


    private string _saveGameSettings;
    private string _saveGameObjects;
    private string _filePath;
    private string _filePathTwo;
    public GameObject ContinueText { get { return _continueText; } }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(Instance);
            Instance = this;
        }
        _filePath = $"{Application.dataPath}/savegamesettings.cgsav";
        _filePathTwo = $"{Application.dataPath}/savegameobjects.cgsav";
    }

    private void Start()
    {
        //When we load into LevelOne and we pressed on Load in the MainMenu, then this will be executed
        if (GameManager.Instance.LoadingSave)
        {
            SetSaveData();
        }
    }

    #region Load And Save
    private void Save()
    {
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

    //Loading the JSON files and writing their data into the SO's
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
        StartCoroutine(nameof(LoadingScreenStart));

        LoadFromJSON();

        //Setting bool to differentiate between starting and loading a game
        GameManager.Instance.LoadingSave = true;

        if (_levelObjects.activeScenes == null)
            return;

        int count = 0;
        for (int i = 0; i < _levelObjects.activeScenes.Length; i++)
        {
            if (_levelObjects.activeScenes[i] != "")
                count++;
        }

        if (count > 1)
            GameManager.Instance.LoadSeveralScenes = true;
        else
            GameManager.Instance.LoadSeveralScenes = false;
    }

    // Setting all states relevant, when loading a game
    private void SetSaveData()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player != null)
        {
            _player.transform.SetPositionAndRotation(_levelObjects.playerPosition, _levelObjects.playerRotation);
            _player.GetComponent<PlayerHealth>().Health = _levelObjects.health;
            _player.GetComponent<PlayerHealth>().CheckHealth(_levelObjects.health);
        }
        if (_lever != null)
            _lever.SetState(_levelObjects.leverPulled);
        if (_door != null)
            _door.SetState(_levelObjects.doorOpen);

        if (_volSlider != null)
            _volSlider.value = _gameSettings.volume;

        if(_volMixer != null)
            _volMixer.SetFloat("volume", _gameSettings.volume);

        foreach (string name in _levelObjects.collectedDiamonds)
        {
            GameObject diamond = GameObject.Find(name);
            if (diamond)
            {
                Destroy(diamond);
            }
        }

        GameManager.Instance.DiamondCount = _levelObjects.diamondCount;
    }

    //Getting into the loading screen
    private IEnumerator LoadingScreenStart()
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("LoadingScreen");
        loadOp.allowSceneActivation = false;

        while (!loadOp.isDone)
        {
            if (loadOp.progress >= 0.9f)
                loadOp.allowSceneActivation = true;

            yield return null;
        }
        yield return null;
    }
    #endregion

    //Calling all save functions
    public void SaveAllData()
    {
        GameManager.Instance.SaveDiamonds();
        _playerHealth.SaveHealth();
        _playerScript.SavePlayerPos();
        _lever.SaveState();
        _door.SaveState();
        UIManager.Instance.SaveVolume();

        Save();
    }
}
