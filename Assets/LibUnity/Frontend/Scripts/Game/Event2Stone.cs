using System;
using UnityEngine;

namespace LibUnity.Frontend
{
    public class Event2Stone : MonoBehaviour
    {
        private Action<Vector3> _callback;
        public void Initialize(Action<Vector3> callback)
        {
            _callback = callback;
        } 
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag.Equals("Ground"))
            {
                _callback?.Invoke(transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}