using System;
using DG.Tweening;
using UnityEngine;

namespace LibUnity.Frontend
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioFadeIn : MonoBehaviour
    {
        [SerializeField] private float duration = 1.0f; 
        
        private AudioSource _audioSource;
        
        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
            var volume = _audioSource.volume;
            _audioSource.volume = 0;
            _audioSource.DOFade(volume, duration);
        }
    }
}