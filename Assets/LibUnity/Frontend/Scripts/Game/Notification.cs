using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private Text _message;
        private Color _color;

        private void Awake()
        {
            _color = _message.color;
        }

        public void Show(string message)
        {
            _message.DOKill();
            _message.text = message;
            _message.color = _color;
            _message.DOFade(0, 2.0f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}