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
        [SerializeField] private Text timeText;
        [SerializeField] private GameObject timeOver;

        [SerializeField] private List<GameObject> stones = new List<GameObject>();
        [SerializeField] private List<GameObject> effects = new List<GameObject>();
        [SerializeField] private Event2Character character;

        [SerializeField] private Image count;
        [SerializeField] private List<Sprite> numbers = new List<Sprite>();
        
        private Action<bool, EventInfo> _result;
        private AudioSource _audioSource;
        
        private float _totalTime = 30;
        private float _timer;
        private int _margin = 100;
        private int _index;
        private bool _timeOver = true;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void Initialize(int index, Action<bool, EventInfo> callback)
        {
            _index = index;
            _result = callback;
            _timer = _totalTime;
            eventIndexText.text = (index + 1).ToString();
            eventContentsText.text = $"제한시간동안 똥을 피해보세요!!";
            timeText.text = _timer.ToString();
            StartCoroutine(Loop());
        }

        private IEnumerator Loop()
        {
            count.sprite = numbers[0];
            yield return new WaitForSeconds(1.0f);
            count.sprite = numbers[1];
            yield return new WaitForSeconds(1.0f);
            count.sprite = numbers[2];
            yield return new WaitForSeconds(1.0f);
            count.gameObject.SetActive(false);
            _timeOver = false;
            
            while (true)
            {
                if (_timeOver)
                {
                    yield break;
                }
                
                var x = Random.Range(_margin, Screen.width - _margin);
                var y = Screen.height + 100;
                var obj = ActiveObject(stones,new Vector3(x, y, 0));
                if (obj != null)
                {
                    var stone = obj.GetComponent<Event2Stone>();
                    stone.Initialize((position) =>
                    {
                        ActiveObject(effects, position);
                    });
                }
                var ratio = _timer / _totalTime;
                var acceleration = Mathf.Max(ratio * ratio, 0.05f);
                yield return new WaitForSeconds(1.0f * acceleration);
            }
        }

        private GameObject ActiveObject(IEnumerable<GameObject> objects, Vector3 position)
        {
            var obj = objects.FirstOrDefault(x => !x.activeSelf);
            if (obj != null)
            {
                obj.transform.position = position;
                obj.gameObject.SetActive(true);
            }
            return obj;
        }

        public void Update()
        {
            if (_timeOver)
            {
                return;
            }

            if (character.IsDead)
            {
                _audioSource.Stop();
                _result?.Invoke(false, new EventInfo(_index, 0, (int)_timer));
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
            _audioSource.Stop();
            yield return new WaitForSeconds(1);
            _result?.Invoke(true, new EventInfo(_index, 0, 0));
        }
    }
}