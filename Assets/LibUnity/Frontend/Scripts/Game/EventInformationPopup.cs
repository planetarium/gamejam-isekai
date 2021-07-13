using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class EventInformationPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI eventText;
        [SerializeField] private Button startButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button bgButton;

        public void Initialize(int index)
        {
            eventText.text = $"{index + 1} Event";
            
            startButton.onClick.AddListener(() =>
            {
                SceneLoader.Instnace.Unload("Lobby");
                SceneLoader.Instnace.Load("Event", () =>
                {
                    Event.Instance.Initialize(index);
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