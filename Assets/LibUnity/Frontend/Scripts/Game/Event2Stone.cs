using UnityEngine;

namespace LibUnity.Frontend
{
    public class Event2Stone : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag.Equals("Ground"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}