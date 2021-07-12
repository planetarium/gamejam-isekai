using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class StageResultPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI storyText;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button bgButton;
        [SerializeField] private List<string> story = new List<string>();
        [SerializeField] private float typingSpeed = 0.1f;

        private Coroutine _coroutine;
        private const string DefaultStory = "스토리가 아직 없습니다.";
        private bool _isDone;

        public void Initialize(bool isSuccess, int index)
        {
            _isDone = false;
            var selectedStory = story.Count > index ? story[index] : DefaultStory;
            resultText.text = isSuccess ? "성공!" : "실패ㅜ";
            if (isSuccess)
            {
                _coroutine = StartCoroutine(TextTyper.Play(storyText, selectedStory, typingSpeed, () =>
                {
                    _isDone = true;
                }));
            }
            else
            {
                _coroutine = StartCoroutine(TextTyper.Rewind(storyText, selectedStory, typingSpeed, () =>
                {
                    _isDone = true;
                }));
            }

            confirmButton.onClick.AddListener(() =>
            {
                Confirm(selectedStory);
            });
            
            bgButton.onClick.AddListener(() =>
            {
                Confirm(selectedStory);
            });
        }

        private void Confirm(string selectedStory)
        {
            if (_isDone)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                storyText.text = selectedStory;
                _isDone = true;
            }
        }

        private void OnDisable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            confirmButton.onClick.RemoveAllListeners();
            bgButton.onClick.RemoveAllListeners();
        }
    }
}