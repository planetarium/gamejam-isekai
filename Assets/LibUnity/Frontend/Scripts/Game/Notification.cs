using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LibUnity.Frontend
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _message;

        public void Show(string message)
        {
            _message.DOKill();
            _message.text = message;
            _message.color = Color.white;
            _message.DOFade(0, 2.0f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}