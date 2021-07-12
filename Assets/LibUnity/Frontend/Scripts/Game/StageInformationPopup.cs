using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class StageInformationPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private Button startButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button bgButton;

        public void Initialize(int index)
        {
            stageText.text = $"{index + 1} 스테이지";
            
            startButton.onClick.AddListener(() =>
            {
                SceneLoader.Instnace.Unload("Lobby");
                SceneLoader.Instnace.Load("Stage", () =>
                {
                    Stage.Instance.Initialize(index);
                });
            });
            
            closeButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
            
            bgButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();
            bgButton.onClick.RemoveAllListeners();
        }
    }
}