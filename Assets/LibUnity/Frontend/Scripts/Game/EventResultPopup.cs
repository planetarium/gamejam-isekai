using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class EventResultPopup : MonoBehaviour
    {
        [SerializeField] private Text eventIndexText;
        [SerializeField] private Text eventContentsText;
        [SerializeField] private Button sendResultButton;

        public void Initialize(bool isSuccess, (int, string) eventInfo)
        {
            var (index, contents) = eventInfo;
            eventIndexText.text = (index + 1).ToString();
            eventContentsText.text = isSuccess ? "점령성공!\n스토리 조각과 리워드를 획득했습니다" : "점령 실패!\n스토리 조각과 리워드를 획득에 실패했습니다";

            sendResultButton.onClick.AddListener(() =>
            {
                SceneLoader.Instnace.Unload("Event");
                if (isSuccess)
                {
                    SceneLoader.Instnace.Load("Story",
                        () => { Story.Instance.Initialize(index, () => Game.Instance.ActionManager.Conquest(index)); });
                }
                else
                {
                    SceneLoader.Instnace.Load("Lobby");
                }
            });
        }
    }
}