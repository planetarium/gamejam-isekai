using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace LibUnity.Frontend
{
    public class Event : MonoBehaviour
    {
        public static Event Instance;

        [FormerlySerializedAs("stageText")] [SerializeField] private TextMeshProUGUI eventText;
        [SerializeField] private List<GameObject> events = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize(int index)
        {
            eventText.text = $"{index + 1} Event";
            LoadStage(index);
        }

        private void LoadStage(int index)
        {
            var id = events.Count > index ? index : 0; 
            var go = Instantiate(events[id], Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);
            go.GetComponent<IEvent>().Initialize(index);
        }
    }
}