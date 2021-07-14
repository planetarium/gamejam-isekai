using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private Image effect;
        
        [SerializeField] private bool isPunch;

        private void OnEnable()
        {
            effect.color = Color.white;
            if (isPunch)
            {
                transform.DOPunchScale(Vector3.one, 0.25f).OnComplete(FadeOut);
            }
            else
            {
                transform.DOShakeScale( 0.25f).OnComplete(FadeOut);
            }
        }

        private void FadeOut()
        {
            effect.DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
        }
    }
}