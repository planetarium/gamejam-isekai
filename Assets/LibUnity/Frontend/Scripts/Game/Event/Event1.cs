using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Event1 : MonoBehaviour, IEvent
    {
        [SerializeField] private Button successButton;
        [SerializeField] private Button failedButton;
    
        public void Initialize(int index)
        {
            successButton.onClick.AddListener((() =>
            {
                SceneLoader.Instnace.Unload("Event");
                SceneLoader.Instnace.Load("Lobby", () => { Lobby.Instance.ShowResult(true, index); });
            }));

            failedButton.onClick.AddListener((() =>
            {
                SceneLoader.Instnace.Unload("Event");
                SceneLoader.Instnace.Load("Lobby", () => { Lobby.Instance.ShowResult(false, index); });
            }));
        }
    }
}