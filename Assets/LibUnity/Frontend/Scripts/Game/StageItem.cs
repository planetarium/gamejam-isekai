using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class StageItem : UIBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI stage;
        [SerializeField] private Image background;
        [SerializeField] private List<Sprite> map;

        private const float Degree = 18;
        private const float Gap = 300;

        protected override void Awake()
        {
            button.onClick.AddListener(ShowStageInfoPopup);
        }

        private void ShowStageInfoPopup()
        {
            var index = int.Parse(stage.text) - 1;
            Lobby.Instance.ShowStageInformation(index);
        }

        public void UpdateItem(int index)
        {
            background.sprite = map[index % 20];
            stage.text = (index + 1).ToString();
            var sinValue = Mathf.Sin(Degree * index * Mathf.Deg2Rad);
            var x = sinValue * sinValue * Gap;
            button.transform.localPosition = new Vector2(x, button.transform.localPosition.y);
        }
    }
}