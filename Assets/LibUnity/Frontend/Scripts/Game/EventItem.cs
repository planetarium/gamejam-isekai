using System.Collections.Generic;
using LibUnity.Backend.State;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ObservableExtensions = UniRx.ObservableExtensions;

namespace LibUnity.Frontend
{
    public class EventItem : UIBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text stageText;
        [SerializeField] private Text conquerorText;
        [SerializeField] private Image background;
        [SerializeField] private GameObject conquerorIcon;
        [SerializeField] private GameObject withConquerorIcon;
        [SerializeField] private GameObject emptyIcon;
        [SerializeField] private List<Sprite> map;

        private const float Degree = 18;
        private const float Gap = 20;
        private const float Revision = 250;
        private const int Cycle = 20;
        private int _index;

        protected override void Awake()
        {
            button.onClick.AddListener(ShowStageInfoPopup);
            ObservableExtensions.Subscribe(Game.Instance.Agent.BlockIndexSubject, SubscribeBlockIndex)
                .AddTo(gameObject);
        }

        private void SubscribeBlockIndex(long blockIndex)
        {
            UpdateItem(_index);
        }

        private void ShowStageInfoPopup()
        {
            var index = int.Parse(stageText.text) - 1;
            Lobby.Instance.ShowStageInformation(index);
        }

        public void UpdateItem(int index)
        {
            _index = index;
            var temp = index % Cycle;
            background.transform.localScale = Vector3.one;
            if (temp > 9)
            {
                temp = Cycle - 1 - temp;
                background.transform.localScale = new Vector3(1, -1, 1);
            }

            background.sprite = map[temp];
            stageText.text = (index + 1).ToString();

            Color color;
            if (Lobby.Instance.TryGetLastHistory(index, out var history))
            {
                if (history.ConquestBlockIndex + StageState.ConquestInterval > Game.Instance.Agent.BlockIndex)
                {
                    ColorUtility.TryParseHtmlString("#580004", out color);    
                    withConquerorIcon.SetActive(true);
                    emptyIcon.SetActive(false);
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#060b3d", out color);
                    withConquerorIcon.SetActive(false);
                    emptyIcon.SetActive(true);
                }

                var address = history.AgentAddress.ToHex().Substring(0, 4);
                conquerorText.text = $"#{address}";
                conquerorIcon.SetActive(true);
            }
            else
            {
                ColorUtility.TryParseHtmlString("#060b3d", out color);
                conquerorText.text = string.Empty;
                withConquerorIcon.SetActive(false);
                emptyIcon.SetActive(true);
                conquerorIcon.SetActive(false);
            }
            conquerorText.GetComponent<Outline>().effectColor = color;
            var value = Mathf.Sin(Degree * index * Mathf.Deg2Rad);
            var x = (Mathf.Abs(value) * Gap * Gap) - Revision;
            button.transform.localPosition = new Vector2(x, button.transform.localPosition.y);
        }
    }
}