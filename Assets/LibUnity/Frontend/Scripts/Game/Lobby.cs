using System.Collections.Generic;
using System.Linq;
using Bencodex.Types;
using Libplanet.Assets;
using LibUnity.Backend;
using LibUnity.Backend.State;
using UniRx;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;
using Text = UnityEngine.UI.Text;

namespace LibUnity.Frontend
{
    public class Lobby : MonoBehaviour
    {
        public static Lobby Instance;

        [SerializeField] private Notification notification;
        [SerializeField] private EventInformationPopup eventInformationPopup;
        [SerializeField] private InfiniteScroll infiniteScroll;
        [SerializeField] private ItemControllerLimited itemControllerLimited;
        [SerializeField] private Text goldText;
        [SerializeField] private Text messageText;
        
        private Currency _currency;
        
        private readonly Dictionary<long, List<StageState.StageHistory>> _stageHistory =
            new Dictionary<long, List<StageState.StageHistory>>();

        private void Awake()
        {
            Instance = this;
            UpdateList();
            UpdateBalance();
            UpdateMessage(Game.Instance.Agent.BlockIndex);
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
            UpdateBalance();
            UpdateMessage(blockIndex);
        }

        private void UpdateBalance()
        {
            var address = Game.Instance.Agent.Address;
            if (_currency.Equals(default))
            {
                _currency = new GoldCurrencyState((Dictionary) Game.Instance.Agent.GetState(Addresses.GoldCurrency)).Currency;
            }

            var balance = Game.Instance.Agent.GetBalance(address, _currency);
            goldText.text = balance.GetQuantityString();
        }

        private void UpdateMessage(long blockIndex)
        {
            messageText.text = $"{blockIndex} 블록 채굴중.. 10 블록마다 새 스테이지가 열립니다.";
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

        public bool TryGetConqueror(int index, out string conqueror)
        {
            conqueror = string.Empty;
            if (!_stageHistory.ContainsKey(index))
            {
                return false;
            }
            
            var histories = _stageHistory[index];
            conqueror = histories.Count > 0 ? $"#{histories.Last().AgentAddress.ToHex().Substring(0, 4)}" : string.Empty;
            return histories.Count > 0;
        }

        public void ShowNotification(string message)
        {
            notification.gameObject.SetActive(true);
            notification.Show(message);
        }
    }
}