using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Lobby : MonoBehaviour
    {
        public static Lobby Instance;

        [SerializeField] private GameObject stagePopup;

        private void Awake()
        {
            Instance = this;
        }

        public void SetActiveStagePopup(bool value)
        {
            stagePopup.SetActive(value);
        }
    }
}