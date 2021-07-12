using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class StageInformationPopup : MonoBehaviour
    {
        [SerializeField] private Button button;

        void Start()
        {
            button.onClick.AddListener(() =>
            {
                SceneLoader.Instnace.Unload("Lobby");
                SceneLoader.Instnace.Load("Stage");
            });
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}