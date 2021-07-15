using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LibUnity.Frontend
{
    public class Event1 : MonoBehaviour, IEvent
    {
        [SerializeField] private Text eventIndexText;
        [SerializeField] private Text eventContentsText;
        [SerializeField] private Text timeText;
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject timeOver;

        [SerializeField] private List<GameObject> planets = new List<GameObject>();
        [SerializeField] private List<GameObject> bombs = new List<GameObject>();
        [SerializeField] private List<GameObject> planetEffects = new List<GameObject>();
        [SerializeField] private List<GameObject> bombEffects = new List<GameObject>();
        
        [SerializeField] private Image count;
        [SerializeField] private List<Sprite> numbers = new List<Sprite>();

        [SerializeField] [Range(0, 99)] private int bombDropProbability = 10;
        [SerializeField] private int targetScore = 500;
        [SerializeField] private int planetScore;
        [SerializeField] private int bombScore;

        private Action<bool, EventInfo> _result;

        private float _totalTime = 30;
        private float _timer;
        private int _margin = 100;
        private int _totalScore;
        private int _index;
        private bool _timeOver = true;
        
        public void Initialize(int index, Action<bool, EventInfo> callback)
        {
            _index = index;
            _result = callback;
            _timer = _totalTime;
            _totalScore = 0;
            eventIndexText.text = (index + 1).ToString();
            eventContentsText.text = $"별풍선을 터뜨려서 {targetScore}점 이상 달성하세요!!";
            timeText.text = _timer.ToString();
            scoreText.text = 0.ToString();

            foreach (var planet in planets)
            {
                planet.GetComponent<Button>().onClick.AddListener(() =>
                {
                    AddScore(planetScore);
                    planet.SetActive(false);
                    ActiveObject(planetEffects, planet.transform.position);
                });
            }

            foreach (var bomb in bombs)
            {
                bomb.GetComponent<Button>().onClick.AddListener(() =>
                {
                    AddScore(bombScore);
                    bomb.SetActive(false);
                    ActiveObject(bombEffects, bomb.transform.position);
                });
            }

            StartCoroutine(Loop());
        }

        private void AddScore(int score)
        {
            _totalScore += score;
            scoreText.text = _totalScore.ToString();
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
                var y = Random.Range(_margin, Screen.height - (_margin * 5));
                ActiveObject(Random.Range(0, 100) > bombDropProbability ? planets : bombs, new Vector3(x, y, 0));
                var ratio = _timer / _totalTime;
                var acceleration = Mathf.Max(ratio * ratio, 0.2f);
                yield return new WaitForSeconds(2.0f * acceleration);
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
            _result?.Invoke(_totalScore > targetScore, new EventInfo(_index, _totalScore, 0));
        }

        private void OnDisable()
        {
            foreach (var planet in planets)
            {
                planet.GetComponent<Button>().onClick.RemoveAllListeners();
            }

            foreach (var bomb in bombs)
            {
                bomb.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }
}