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
                    SceneLoader.Instnace.ChangeScene("Event", "Story",
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
                    SceneLoader.Instnace.ChangeScene("Event", "Lobby");
                });
            }
        }
    }
}