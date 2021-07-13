using UnityEngine;
using UnityEngine.Serialization;

namespace LibUnity.Frontend
{
    public class Lobby : MonoBehaviour
    {
        public static Lobby Instance;

        [FormerlySerializedAs("stageInformationPopup")] [SerializeField] private EventInformationPopup eventInformationPopup;
        [FormerlySerializedAs("stageResultPopup")] [SerializeField] private EventResultPopup eventResultPopup;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowStageInformation(int index)
        {
            eventInformationPopup.gameObject.SetActive(true);
            eventInformationPopup.Initialize(index);
        }

        public void ShowResult(bool isSuccess, int index)
        {
            eventResultPopup.gameObject.SetActive(true);
            eventResultPopup.Initialize(isSuccess, index);
        }
    }
}