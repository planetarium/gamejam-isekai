using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Prologue : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Button skipButton;
        [TextArea] [SerializeField] private string synopsis = "스토리를 넣어주세요"; 
        [SerializeField] private float typingSpeed = 0.1f;

        private Coroutine _coroutine;
        private bool _isDone;

        private void Start()
        {
            _coroutine = StartCoroutine(TextTyper.Play(text, synopsis, typingSpeed, (isSuccess) =>
            {
                _isDone = isSuccess;
                SceneLoader.Instnace.Unload("Prologue");
                SceneLoader.Instnace.Load("Lobby");
            }));

            skipButton.onClick.AddListener(() =>
            {
                if (_isDone)
                {
                    SceneLoader.Instnace.Unload("Prologue");
                    SceneLoader.Instnace.Load("Lobby");
                    return;
                }

                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                text.text = synopsis;
                _isDone = true;
            });
        }
    }
}