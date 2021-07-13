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
        private const float Gap = 20;
        private const float Revision = 150;
        private const int Cycle = 20;

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
            var temp = index % Cycle;
            background.transform.localScale = Vector3.one;
            if (temp > 9)
            {
                temp = Cycle - 1 - temp;
                background.transform.localScale = new Vector3(1, -1, 1);
            }

            background.sprite = map[temp];
            stage.text = (index + 1).ToString();
            var value = Mathf.Sin(Degree * index * Mathf.Deg2Rad);
            
            var x = (Mathf.Abs(value) * Gap * Gap) - Revision;
            button.transform.localPosition = new Vector2(x, button.transform.localPosition.y);
        }
    }
}