using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 20f;

        #region Monobehaviour
        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameWon += StopPlayer;
        }

        private void OnDisable()
        {
            if (!GameManager.IsInitialized)
            {
                return;
            }
            GameManager.Instance.OnGameWon -= StopPlayer;
        }
        #endregion

        public void SetSpeed(float speed)
        {
            // Lerp here?
            _speed = speed;
        }

        private void StopPlayer()
        {
            SetSpeed(0f);
        }
    }
}