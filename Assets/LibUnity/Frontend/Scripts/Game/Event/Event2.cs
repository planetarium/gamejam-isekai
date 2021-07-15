using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LibUnity.Frontend
{
    public class Event2 : MonoBehaviour, IEvent
    {
        [SerializeField] private Text eventIndexText;
        [SerializeField] private Text eventContentsText;
        [SerializeField] private Text countdownText;
        [SerializeField] private Text timeText;
        [SerializeField] private GameObject timeOver;

        [SerializeField] private List<GameObject> stones = new List<GameObject>();
        [SerializeField] private Event2Character character;
        
        private Action<bool> _result;

        private float _totalTime = 30;
        private float _timer;
        private int _margin = 100;
        private bool _timeOver = true;
        
        public void Initialize(int index, Action<bool> callback)
        {
            _result = callback;
            _timer = _totalTime;
            eventContentsText.text = $"제한시간동안 돌을 피해보세요!!";
            timeText.text = _timer.ToString();
            StartCoroutine(Loop());
        }

        private IEnumerator Loop()
        {
            countdownText.text = "3";
            yield return new WaitForSeconds(1.0f);
            countdownText.text = "2";
            yield return new WaitForSeconds(1.0f);
            countdownText.text = "1";
            yield return new WaitForSeconds(1.0f);
            countdownText.text = "START!";
            yield return new WaitForSeconds(1.0f);
            countdownText.gameObject.SetActive(false);
            _timeOver = false;
            
            while (true)
            {
                if (_timeOver)
                {
                    yield break;
                }
                
                var x = Random.Range(_margin, Screen.width - _margin);
                var y = Screen.height + 100;
                ActiveObject(stones,new Vector3(x, y, 0));
                var ratio = _timer / _totalTime;
                var acceleration = Mathf.Max(ratio * ratio, 0.05f);
                yield return new WaitForSeconds(1.0f * acceleration);
            }
        }

        private void ActiveObject(IEnumerable<GameObject> objects, Vector3 position)
        {
            var obj = objects.FirstOrDefault(x => !x.activeSelf);
            if (obj != null)
            {
                obj.transform.position = position;
                obj.gameObject.SetActive(true);
            }
        }

        public void Update()
        {
            if (_timeOver)
            {
                return;
            }

            if (character.IsDead)
            {
                _result?.Invoke(false);
                _timeOver = true;
            }
            
            _timer -= Time.unscaledDeltaTime;
            if (_timer <= 0)
            {
                _timer = 0;
                _timeOver = true;
                timeOver.gameObject.SetActive(true);
                StartCoroutine(ShowResult());
            }
            
            var time = (int) _timer;
            timeText.text = time.ToString();
        }

        private IEnumerator ShowResult()
        {
            yield return new WaitForSeconds(1);
            _result?.Invoke(true);
        }
    }
}