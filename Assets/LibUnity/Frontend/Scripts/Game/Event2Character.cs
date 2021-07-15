using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LibUnity.Frontend
{
    public class Event2Character : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private Vector2 _direction;
        private float _speed = 400;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Dead = Animator.StringToHash("Dead");

        public bool IsDead { get; private set; }

        private void Awake()
        {
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
            animator.SetTrigger(_direction.sqrMagnitude > 0f ? Move : Idle);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag.Equals("Stone"))
            {
                IsDead = true;
                animator.SetTrigger(Dead);
                transform.DOShakePosition(5,3, 50);
            }
        }
    }
}