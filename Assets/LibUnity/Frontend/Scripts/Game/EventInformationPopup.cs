using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibUnity.Backend.Action;
using LibUnity.Backend.State;
using LibUnity.Frontend.BlockChain;
using LibUnity.Frontend.State;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class EventInformationPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI eventIndexText;
        [SerializeField] private TextMeshProUGUI eventContent;
        [SerializeField] private TextMeshProUGUI eventHistory;
        [SerializeField] private Button startButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button bgButton;

        public void Initialize(int index, IEnumerable<StageState.StageHistory> histories)
        {
            eventIndexText.text = $"{index + 1}";
            eventContent.text = $"{index + 1} 이벤트 설명은 아직 준비중입니다";
            var sb = new StringBuilder();
            foreach (var history in histories)
            {
                var conquestBlockIndex = history.ConquestBlockIndex;
                var address = history.AgentAddress.ToHex().Substring(0, 4);
                sb.Append($"{conquestBlockIndex} 블록 : {address}가 {StageState.ConquestInterval}블록동안 점령\n");
            }
            eventHistory.text = sb.ToString();

            startButton.onClick.AddListener(() =>
            {
                Debug.Log($"current : {Game.Instance.Agent.BlockIndex}");
                if (histories.Any() &&
                    histories.Last().ConquestBlockIndex + StageState.ConquestInterval> Game.Instance.Agent.BlockIndex)
                {
                    var standard = histories.Last().ConquestBlockIndex + StageState.ConquestInterval;
                    Lobby.Instance.ShowNotification($"아직 점령할 수 없습니다 {standard}블록 이후부터 점령가능");
                    return;
                }
                
                SceneLoader.Instnace.Unload("Lobby");
                SceneLoader.Instnace.Load("Event", () =>
                {
                    Event.Instance.Initialize(index, eventContent.text);
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