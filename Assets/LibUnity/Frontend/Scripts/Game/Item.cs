using System.Collections;
using UnityEngine;

namespace LibUnity.Frontend
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private float hideTime = 5.0f;

        private Coroutine _coroutine;
        private void OnEnable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(Hide(5.0f));
        }

        private void OnDisable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator Hide(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }
}