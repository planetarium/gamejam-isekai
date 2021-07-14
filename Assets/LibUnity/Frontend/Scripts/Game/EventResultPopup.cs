using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class EventResultPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI eventIndexText;
        [SerializeField] private TextMeshProUGUI eventContentsText;
        [SerializeField] private GameObject successContainer;
        [SerializeField] private GameObject failedContainer;
        [SerializeField] private Button sendResultButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button mainButton;

        public void Initialize(bool isSuccess, (int, string) eventInfo)
        {
            var (index, contents) = eventInfo;
            eventIndexText.text = (index + 1).ToString();
            eventContentsText.text = contents;
            if (isSuccess)
            {
                successContainer.SetActive(true);
                failedContainer.SetActive(false);
                sendResultButton.onClick.AddListener(() =>
                {
                    SceneLoader.Instnace.Unload("Event");
                    SceneLoader.Instnace.Load("Story", () =>
                    {
                        Story.Instance.Initialize(index, () => Game.Instance.ActionManager.Conquest(index));
                    });
                    
                });
            }
            else
            {
                successContainer.SetActive(false);
                failedContainer.SetActive(true);
                retryButton.onClick.AddListener(() => { });

                mainButton.onClick.AddListener(() =>
                {
                    SceneLoader.Instnace.Unload("Event");
                    SceneLoader.Instnace.Load("Lobby");
                });
            }
        }
    }
}