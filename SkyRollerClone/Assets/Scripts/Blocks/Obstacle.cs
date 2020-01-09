using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    [RequireComponent(typeof(AudioSource))]
    public class Obstacle : MonoBehaviour
    {
        private AudioSource _audioSource;

        #region Monobehaviour
        private void Start()
        {
            Init();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _audioSource.Play();
                GameManager.Instance.GameLost();
            }
        }
        #endregion

        private void Init()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}