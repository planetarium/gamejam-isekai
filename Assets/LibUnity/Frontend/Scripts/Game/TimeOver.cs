using DG.Tweening;
using UnityEngine;

namespace LibUnity.Frontend
{
    public class TimeOver : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.localPosition = new Vector3(0, -Screen.height, 0);
            transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
            {
                transform.DOPunchScale(Vector3.one, 0.25f);
            });
        }
    }
}