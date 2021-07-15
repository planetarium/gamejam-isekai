using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instnace;
        
        [SerializeField] private Image loading;
        [SerializeField] private Image fade;

        private void Awake()
        {
            Instnace = this;
        }

        public void Load(string sceneName, Action callback = null)
        {
            StartCoroutine(LoadScene(sceneName, callback));
        }
        
        public void Unload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        private IEnumerator LoadScene(string sceneName, Action callback = null)
        {
            fade.color = Color.black;
            loading.fillAmount = 0;
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!operation.isDone)
            {
                yield return null;
                loading.fillAmount = operation.progress;
            }
            loading.fillAmount = 0;
            callback?.Invoke();
            fade.DOFade(0, 1);
        }

        public void ChangeScene(string unloadSceneName, string loadSceneName, Action callback = null)
        {
            fade.color = Color.clear;
            fade.DOFade(1, 1).OnComplete(() =>
            {
                SceneManager.UnloadSceneAsync(unloadSceneName);
                StartCoroutine(LoadScene(loadSceneName, callback));
            });
        }
    }
}