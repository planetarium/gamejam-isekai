using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LibUnity.Frontend
{
    public class Event2Character : MonoBehaviour
    {
        private float _speed = 400;
        private Vector2 _direction;
        [SerializeField] private GameObject idle;
        [SerializeField] private GameObject dead;
        
        public bool IsDead { get; private set; }

        private void Awake()
        {
            idle.SetActive(true);
            dead.SetActive(false);
            IsDead = false;
        }

        private void Update()
        {
            transform.localScale = _direction.x >= 0 ? new Vector3(-1, 1, 1) : Vector3.one;
            transform.Translate(_direction * (_speed * Time.deltaTime), Space.World);
            var xPos = Mathf.Clamp(transform.position.x, 50, Screen.width - 50);
            transform.position = new Vector3(xPos, transform.position.y, 0);
        }

        public void OnMovement(InputValue value)
        {
            var inputMovement = value.Get<Vector2>();
            _direction = new Vector2(inputMovement.x, 0);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag.Equals("Stone"))
            {
                IsDead = true;
                idle.SetActive(false);
                dead.SetActive(true);
                transform.DOShakePosition(5,3, 50);
            }
        }
    }
}