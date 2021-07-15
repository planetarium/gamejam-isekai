using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class EventResultPopup : MonoBehaviour
    {
        [SerializeField] private Text eventContentsText;
        [SerializeField] private Text rewardsText;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text timeText;
        [SerializeField] private Button sendResultButton;
        [SerializeField] private GameObject success;
        [SerializeField] private GameObject failed;

        public void Initialize(bool isSuccess, EventInfo eventInfo)
        {
            timeText.text = eventInfo.Time.ToString();
            scoreText.text = eventInfo.Score.ToString();
            
            if (isSuccess)
            {
                rewardsText.text = "50";
                sendResultButton.onClick.AddListener(() =>
                {
                    SceneLoader.Instnace.Unload("Event");
                    SceneLoader.Instnace.Load("Story",
                        () =>
                        {
                            Story.Instance.Initialize(eventInfo.Index,
                                () => Game.Instance.ActionManager.Conquest(eventInfo.Index));
                        });
                });
            }
            else
            {
                rewardsText.text = "0";
                sendResultButton.onClick.AddListener(() =>
                {
                    SceneLoader.Instnace.Unload("Event");
                    SceneLoader.Instnace.Load("Lobby");
                });
            }

            // eventContentsText.text = isSuccess ? "점령성공!\n스토리 조각과 리워드를 획득했습니다" : "점령 실패!\n스토리 조각과 리워드를 획득에 실패했습니다";
        }
    }
}