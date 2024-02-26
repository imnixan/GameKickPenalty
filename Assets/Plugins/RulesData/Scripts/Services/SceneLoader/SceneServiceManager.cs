using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneServiceManager
    {
        public void LoadScene(string sceneName, Action onLoaded = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.completed += _ => onLoaded?.Invoke();
        }
    }
}
