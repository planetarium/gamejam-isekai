using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class StageItem : UIBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text stage;

        protected override void Awake()
        {
            button.onClick.AddListener(ShowStageInfoPopup);
        }

        private static void ShowStageInfoPopup()
        {
            Lobby.Instance.SetActiveStagePopup(true);
        }

        public void UpdateItem(int index)
        {
            stage.text = $"스테이지 : {index}";
        }
    }
}