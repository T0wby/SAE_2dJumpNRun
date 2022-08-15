using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingBar;
    [SerializeField] private SO_LevelObjects _levelObjects;
    private int _sceneToLoad;

    private void Start()
    {
        if (!GameManager.Instance.LoadSeveralScenes)
        {
            _sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(nameof(LoadingScreenStart));
        }
        else
        {
            StartCoroutine(nameof(LoadingSeveralScenes));
        }
    }

    private IEnumerator LoadingScreenStart()
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(_sceneToLoad);
        loadOp.allowSceneActivation = false;

        while (!loadOp.isDone)
        {
            _loadingBar.fillAmount = loadOp.progress;

            if (loadOp.progress >= 0.9f)
            {
                _loadingBar.fillAmount = 1f;
                SaveGameManager.Instance.ContinueText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                    // Activate the Scene
                    loadOp.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.UnloadSceneAsync("LoadingScreen");
    }

    private IEnumerator LoadingSeveralScenes()
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(_levelObjects.activeScenes[0]);
        loadOp.allowSceneActivation = false;

        while (!loadOp.isDone)
        {
            _loadingBar.fillAmount = loadOp.progress;

            if (loadOp.progress >= 0.9f)
            {
                
                _loadingBar.fillAmount = 1f;
                SaveGameManager.Instance.ContinueText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Activate the Scene
                    StartCoroutine(nameof(LoadingAdditiveScenes));
                    loadOp.allowSceneActivation = true;
                }
            }

            yield return null;
        }

        SceneManager.UnloadSceneAsync("LoadingScreen");
        yield return null;
    }

    private IEnumerator LoadingAdditiveScenes()
    {
        for (int i = 1; i < _levelObjects.activeScenes.Length; i++)
        {
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(_levelObjects.activeScenes[i], LoadSceneMode.Additive);

            while (!loadOp.isDone)
            {
                _loadingBar.fillAmount = loadOp.progress;
                yield return null;
            }
        }
    }
}
