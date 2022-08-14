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
}
