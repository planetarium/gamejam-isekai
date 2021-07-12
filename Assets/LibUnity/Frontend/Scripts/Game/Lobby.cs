using UnityEngine;

namespace LibUnity.Frontend
{
    public class Lobby : MonoBehaviour
    {
        public static Lobby Instance;

        [SerializeField] private StageInformationPopup stageInformationPopup;
        [SerializeField] private StageResultPopup stageResultPopup;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowStageInformation(int index)
        {
            stageInformationPopup.gameObject.SetActive(true);
            stageInformationPopup.Initialize(index);
        }

        public void ShowResult(bool isSuccess, int index)
        {
            stageResultPopup.gameObject.SetActive(true);
            stageResultPopup.Initialize(isSuccess, index);
        }
    }
}