using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibUnity.Backend.State;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class EventInformationPopup : MonoBehaviour
    {
        [SerializeField] private GameObject firstEventContainer;
        [SerializeField] private GameObject secondEventContainer;
        [SerializeField] private Text eventIndexText;
        [SerializeField] private Text eventHistory;
        [SerializeField] private Button startButton;
        [SerializeField] private Button closeButton;

        public void Initialize(int index, IEnumerable<StageState.StageHistory> histories)
        {
            firstEventContainer.SetActive(index % 2 == 0);
            secondEventContainer.SetActive(index % 2 == 1);
            eventIndexText.text = $"{index + 1}";
            var sb = new StringBuilder();
            foreach (var history in histories)
            {
                var conquestBlockIndex = history.ConquestBlockIndex;
                var address = history.AgentAddress.ToHex().Substring(0, 4);
                sb.Append($"{conquestBlockIndex} 블록 : #{address}가 {StageState.ConquestInterval}블록동안 점령\n");
            }
            eventHistory.text = sb.ToString();

            startButton.onClick.AddListener(() =>
            {
                SceneLoader.Instnace.ChangeScene("Lobby", "Event", () =>
                {
                    Event.Instance.Initialize(index);
                });
            });

            closeButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();
        }
    }
}