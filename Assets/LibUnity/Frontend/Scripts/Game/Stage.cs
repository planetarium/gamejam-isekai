using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Stage : MonoBehaviour
    {
        public static Stage Instance;
        [SerializeField] private Button tempExit;

        private void Awake()
        {
            Instance = this;
            tempExit.onClick.AddListener((() =>
            {
                SceneLoader.Instnace.Unload("Stage");
                SceneLoader.Instnace.Load("Lobby");
            }));
        }
    }
}