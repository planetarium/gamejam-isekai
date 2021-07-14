using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        private const float Revision = 150;
        private const int Cycle = 20;

        protected override void Awake()
        {
            button.onClick.AddListener(ShowStageInfoPopup);
        }

        private void ShowStageInfoPopup()
        {
            var index = int.Parse(stageText.text) - 1;
            Lobby.Instance.ShowStageInformation(index);
        }

        public void UpdateItem(int index)
        {
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
            if (Lobby.Instance.TryGetConqueror(index, out var conqueror))
            {
                ColorUtility.TryParseHtmlString("#580004", out color);
                conquerorText.text = conqueror;
                withConquerorIcon.SetActive(true);
                emptyIcon.SetActive(false);
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