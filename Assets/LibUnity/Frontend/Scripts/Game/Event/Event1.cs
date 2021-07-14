using System;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Event1 : MonoBehaviour, IEvent
    {
        [SerializeField] private Button successButton;
        [SerializeField] private Button failedButton;

        private Action<bool> _result;
        
        public void Initialize(Action<bool> callback)
        {
            _result = callback;
            
            successButton.onClick.AddListener((() =>
            {
                callback?.Invoke(true);
                
                // Event.Instance.ShowResult(true, index, contents);
                
                
                // SceneLoader.Instnace.Unload("Event");
                // SceneLoader.Instnace.Load("Lobby");
            }));

            failedButton.onClick.AddListener((() =>
            {
                callback?.Invoke(false);
                // Event.Instance.ShowResult(false, index, contents);
                // SceneLoader.Instnace.Unload("Event");
                // SceneLoader.Instnace.Load("Lobby", () => {  });
            }));
        }
    }
}