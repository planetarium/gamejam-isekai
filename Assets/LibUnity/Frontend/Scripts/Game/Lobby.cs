using System.Collections.Generic;
using System.Linq;
using Bencodex.Types;
using LibUnity.Backend.State;
using UniRx;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;

namespace LibUnity.Frontend
{
    public class Lobby : MonoBehaviour
    {
        public static Lobby Instance;

        [SerializeField] private Notification notification;
        [SerializeField] private EventInformationPopup eventInformationPopup;
        [SerializeField] private InfiniteScroll infiniteScroll;
        [SerializeField] private ItemControllerLimited itemControllerLimited;

        private readonly Dictionary<long, List<StageState.StageHistory>> _stageHistory =
            new Dictionary<long, List<StageState.StageHistory>>();

        private void Awake()
        {
            Instance = this;
            UpdateList();
            ObservableExtensions.Subscribe(Game.Instance.Agent.BlockIndexSubject, SubscribeBlockIndex)
                .AddTo(gameObject);
        }

        public void ShowStageInformation(int index)
        {
            if (!_stageHistory.ContainsKey(index))
            {
                return;
            }

            var histories = _stageHistory[index];

            eventInformationPopup.gameObject.SetActive(true);
            eventInformationPopup.Initialize(index, _stageHistory[index]);
        }

        private void SubscribeBlockIndex(long blockIndex)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            _stageHistory.Clear();
            var index = 0;
            while (true)
            {
                var state = Game.Instance.Agent.GetState(StageState.Derive(index));
                if (state is null)
                {
                    break;
                }

                var stageState = new StageState((Dictionary) state);
                if (!_stageHistory.ContainsKey(index))
                {
                    _stageHistory.Add(index, stageState.Histories);
                }

                index++;
            }

            itemControllerLimited.Max = index;
            infiniteScroll.Reset();
        }

        public string GetConqueror(int index)
        {
            if (!_stageHistory.ContainsKey(index))
            {
                return string.Empty;
            }
            
            var histories = _stageHistory[index];
            return histories.Count > 0 ? histories.Last().AgentAddress.ToHex().Substring(0, 4) : string.Empty;
        }

        public void ShowNotification(string message)
        {
            notification.gameObject.SetActive(true);
            notification.Show(message);
        }
    }
}