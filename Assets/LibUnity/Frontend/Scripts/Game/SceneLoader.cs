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

        private Coroutine _volumeCoroutine;
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

            if (_volumeCoroutine != null)
            {
                StopCoroutine(_volumeCoroutine);
            }
            _volumeCoroutine = StartCoroutine(FadeVolume(true));
            fade.DOFade(0, 1);
        }

        public void ChangeScene(string unloadSceneName, string loadSceneName, Action callback = null)
        {
            if (_volumeCoroutine != null)
            {
                StopCoroutine(_volumeCoroutine);
            }
            _volumeCoroutine = StartCoroutine(FadeVolume(false));
            fade.color = Color.clear;
            fade.DOFade(1, 1).OnComplete(() =>
            {
                SceneManager.UnloadSceneAsync(unloadSceneName);
                StartCoroutine(LoadScene(loadSceneName, callback));
            });
        }

        private IEnumerator FadeVolume(bool isIn)
        {
            const float tick = 0.02f;
            if (isIn)
            {
                AudioListener.volume = 0;
                while (AudioListener.volume < 1)
                {
                    yield return new WaitForSeconds(tick);
                    AudioListener.volume += tick;
                }
                AudioListener.volume = 1;
            }
            else
            {
                AudioListener.volume = 1;
                while (AudioListener.volume > 0)
                {
                    yield return new WaitForSeconds(tick);
                    AudioListener.volume -= tick;
                }
                AudioListener.volume = 0;
            }
        }
    }
}