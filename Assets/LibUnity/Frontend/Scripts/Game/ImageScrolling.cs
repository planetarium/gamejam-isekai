using UnityEngine;

namespace LibUnity.Frontend
{
    public class ImageScrolling : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void FixedUpdate()
        {
            transform.localPosition += Vector3.up * (speed * Time.deltaTime);
            if (speed > 0)
            {
                if(transform.localPosition.y > Screen.height)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, -Screen.height, 0);
                }
            }
            else
            {
                if(transform.localPosition.y <= -Screen.height)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, Screen.height, 0);
                }
            }
        }
    }
}