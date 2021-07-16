using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LibUnity.Frontend
{
    public class ImageListScrolling : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> images = new List<RectTransform>();
        [SerializeField] private float speed;
        private float _size;

        private void Awake()
        {
            _size = images.First().rect.height;
        }

        private void FixedUpdate()
        {
            var value = Vector2.up * (speed * Time.deltaTime);

            foreach (var image in images)
            {
                image.anchoredPosition += value;
                if (speed > 0)
                {
                    if(image.anchoredPosition.y > _size)
                    {
                        image.anchoredPosition -= new Vector2(0, _size * 2);
                    }
                }
                else
                {
                    if(image.anchoredPosition.y <= -(_size))
                    {
                        image.anchoredPosition += new Vector2(0, _size * 2);
                    }
                }
            }
        }
    }
}