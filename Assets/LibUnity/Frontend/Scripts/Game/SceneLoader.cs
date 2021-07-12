using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instnace;
        
        [SerializeField] private Image loading;

        private void Awake()
        {
            Instnace = this;
        }

        public void Load(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));
        }
        
        public void Unload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            loading.fillAmount = 0;
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!operation.isDone)
            {
                yield return null;
                loading.fillAmount = operation.progress;
            }
            loading.fillAmount = 0;
        }
    }
}