using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Story : MonoBehaviour
    {
        [Serializable]
        public class StoryInfo
        {
            [TextArea] public string title;
            [TextArea] public string content;
        }
        
        public static Story Instance;

        [SerializeField] private Text eventIndexText;
        [SerializeField] private Text storyTitleText;
        [SerializeField] private Text storyText;
        [SerializeField] private Text progressText;
        [SerializeField] private Slider progressBar;
        [SerializeField] private List<StoryInfo> story = new List<StoryInfo>();
        [SerializeField] private float typingPlaySpeed = 0.1f;
        [SerializeField] private float typingRewindSpeed = 0.1f;
        [SerializeField] private Button confirmButton;

        private Coroutine _coroutine;
        private string _selectedStory;
        private int _index;
        private bool _isDone;
        private bool _isSuccess;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize(int index, Action action = null)
        {
            action?.Invoke();
            _index = index;
            _isDone = false;
            eventIndexText.text = (index + 1).ToString();

            var idx = story.Count > index ? index : 0;
            storyTitleText.text = story[idx].title;
            _selectedStory = story[idx].content; 
            _coroutine = StartCoroutine(TextTyper.PlayWithResource(storyText, _selectedStory, typingPlaySpeed,
                progressBar,
                progressText, ActionRenderResult));

            confirmButton.onClick.AddListener(Confirm);
        }

        private void Confirm()
        {
            if (!_isDone)
            {
                return;
            }

            SceneLoader.Instnace.ChangeScene("Story", "Lobby", () =>
            {
                Lobby.Instance.ShowActionResultPopup(_index, _isSuccess);
            });
        }

        private void ActionRenderResult(bool isSuccess)
        {
            if (_coroutine == null)
            {
                return;
            }

            StopCoroutine(_coroutine);
            _isSuccess = isSuccess;
            if (isSuccess)
            {
                storyText.text = _selectedStory;
                _isDone = true;
            }
            else
            {
                var currentWrittenStory = storyText.text;
                StartCoroutine(TextTyper.Rewind(storyText, _selectedStory,
                    currentWrittenStory, typingRewindSpeed,
                    progressBar, progressText, (isSuccess) => { _isDone = true; }));
            }
        }
    }
}