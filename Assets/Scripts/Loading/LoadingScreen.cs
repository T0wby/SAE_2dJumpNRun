using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingBar;
    private int _sceneToLoad;

    private void Start()
    {
        _sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(nameof(LoadingScreenStart));
    }

    private IEnumerator LoadingScreenStart()
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(_sceneToLoad);

        while (!loadOp.isDone)
        {
            _loadingBar.fillAmount = loadOp.progress;
            yield return null;
        }

        _loadingBar.fillAmount = 1f;

        SceneManager.UnloadSceneAsync("LoadingScreen");
    }
}
