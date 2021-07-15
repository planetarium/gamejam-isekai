using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Story : MonoBehaviour
    {
        public static Story Instance;

        [SerializeField] private Text eventIndexText;
        [SerializeField] private Text storyThemeText;
        [SerializeField] private Text storyText;
        [SerializeField] private Text progressText;
        [SerializeField] private Slider progressBar;
        [TextArea] [SerializeField] private List<string> story = new List<string>();
        [SerializeField] private float typingPlaySpeed = 0.1f;
        [SerializeField] private float typingRewindSpeed = 0.1f;
        [SerializeField] private Button confirmButton;

        private Coroutine _coroutine;
        private string _selectedStory;
        private bool _isDone;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize(int index, Action action = null)
        {
            action?.Invoke();
            _isDone = false;
            eventIndexText.text = (index + 1).ToString();
            storyThemeText.text = "스토리 제목을 넣어주세요";

            _selectedStory = story.Count > index ? story[index] : story[0];
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

            SceneLoader.Instnace.Unload("Story");
            SceneLoader.Instnace.Load("Lobby");
        }

        private void ActionRenderResult(bool isSuccess)
        {
            if (_coroutine == null)
            {
                return;
            }

            StopCoroutine(_coroutine);
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