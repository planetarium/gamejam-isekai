using System;
using UnityEngine;

namespace LibUnity.Frontend
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioFade : MonoBehaviour
    {
        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}