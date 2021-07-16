using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LibUnity.Frontend
{
    public class Event : MonoBehaviour
    {
        public static Event Instance;

        [SerializeField] private List<GameObject> events = new List<GameObject>();
        [SerializeField] private EventResultPopup eventResultPopup;

        
        private void Awake()
        {
            Instance = this;
        }

        
        public void Initialize(int index)
        {
            LoadStage(index);
        }

        private void LoadStage(int index)
        {
            // var id = events.Count > index ? index : 0; 
            // var go = Instantiate(events[id], Vector3.zero, Quaternion.identity);
            var go = Instantiate(events[index % 2], Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);
            go.GetComponent<IEvent>().Initialize(index, ShowResult);
        }

        private void ShowResult(bool isSuccess, EventInfo eventInfo)
        {
            eventResultPopup.gameObject.SetActive(true);
            eventResultPopup.Initialize(isSuccess, eventInfo);
        }
    }
}