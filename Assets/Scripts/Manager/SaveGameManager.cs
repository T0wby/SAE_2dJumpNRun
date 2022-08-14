using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager Instance { get; private set; }

    [SerializeField] private SO_GameSettings _gameSettings;
    [SerializeField] private SO_LevelObjects _levelObjects;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _continueText;

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
        StartCoroutine(nameof(LoadingScreenStart));

        LoadFromJSON();

        if (_levelObjects.activeScenes == null)
            return;
        for (int i = 0; i < _levelObjects.activeScenes.Length; i++)
        {
            switch (i)
            {
                case 0:
                    StartCoroutine(LoadingLevel(_levelObjects.activeScenes[i], _loadingBar));
                    break;
                case 1:
                    if(_levelObjects.activeScenes[i] != "")
                        SceneManager.LoadSceneAsync(_levelObjects.activeScenes[i], LoadSceneMode.Additive);
                    break;
                default:
                    break;
            }
        }

        StartCoroutine(nameof(UnloadLoadingScreen));

        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player != null)
        {
            _player.transform.SetPositionAndRotation(_levelObjects.player.position, _levelObjects.player.rotation);
            _player.GetComponent<PlayerHealth>().Health = _levelObjects.health;
        }

        GameManager.Instance.DiamondCount = _levelObjects.diamondCount;
    }

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

    private IEnumerator UnloadLoadingScreen()
    {
        AsyncOperation loadOp = SceneManager.UnloadSceneAsync("LoadingScreen");

        while (!loadOp.isDone)
        {
            yield return null;
        }
        yield return null;
    }

    private IEnumerator LoadingLevel(string levelName, Image loadingBar)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(levelName);
        loadOp.allowSceneActivation = false;

        while (!loadOp.isDone)
        {
            loadingBar.fillAmount = loadOp.progress;

            if (loadOp.progress >= 0.9f)
            {
                loadingBar.fillAmount = 1f;
                _continueText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                    // Activate the Scene
                    loadOp.allowSceneActivation = true;
            }

            yield return null;
        }
        yield return null;

    }
}
