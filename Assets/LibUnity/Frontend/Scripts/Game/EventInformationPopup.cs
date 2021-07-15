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
        [SerializeField] private Text eventIndexText;
        [SerializeField] private Text eventTheme;
        [SerializeField] private Text eventHistory;
        [SerializeField] private Button startButton;
        [SerializeField] private Button closeButton;

        public void Initialize(int index, IEnumerable<StageState.StageHistory> histories)
        {
            eventIndexText.text = $"{index + 1}";
            eventTheme.text = $"별똥별 터뜨리기";
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
                Debug.Log($"current : {Game.Instance.Agent.BlockIndex}");
                if (histories.Any() &&
                    histories.Last().ConquestBlockIndex + StageState.ConquestInterval> Game.Instance.Agent.BlockIndex)
                {
                    var name = histories.Last().AgentAddress.ToHex().Substring(0, 4);
                    // var standard = histories.Last().ConquestBlockIndex + StageState.ConquestInterval;
                    Lobby.Instance.ShowNotification($"#{name}가 점령하고 있습니다.");
                    return;
                }
                
                SceneLoader.Instnace.Unload("Lobby");
                SceneLoader.Instnace.Load("Event", () =>
                {
                    Event.Instance.Initialize(index, eventTheme.text);
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