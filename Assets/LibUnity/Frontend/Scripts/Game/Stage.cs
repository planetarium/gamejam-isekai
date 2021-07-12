using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Stage : MonoBehaviour
    {
        public static Stage Instance;

        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private Button successButton;
        [SerializeField] private Button failedButton;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize(int index)
        {
            stageText.text = $"{index + 1} 스테이지";

            successButton.onClick.AddListener((() =>
            {
                SceneLoader.Instnace.Unload("Stage");
                SceneLoader.Instnace.Load("Lobby", () => { Lobby.Instance.ShowResult(true, index); });
            }));

            failedButton.onClick.AddListener((() =>
            {
                SceneLoader.Instnace.Unload("Stage");
                SceneLoader.Instnace.Load("Lobby", () => { Lobby.Instance.ShowResult(false, index); });
            }));
        }

        private void OnDisable()
        {
            successButton.onClick.RemoveAllListeners();
            failedButton.onClick.RemoveAllListeners();
        }
    }
}