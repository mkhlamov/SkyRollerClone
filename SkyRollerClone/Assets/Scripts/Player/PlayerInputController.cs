using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyRollerClone.Input;

namespace SkyRollerClone.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField]
        private float _swipeSensitivity = 100f;
        [SerializeField]
        private float _currentLegSpread = 0f;
        [SerializeField]
        private Animator _animator;
        private PlayerMovement _playerMovement;

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            SwipeDetector.OnSwipe += SwipeHandler;
        }

        private void OnDisable()
        {
            SwipeDetector.OnSwipe -= SwipeHandler;
        }

        public void ResetLegSpread()
        {
            _currentLegSpread = 0f;
        }

        private void SwipeHandler(SwipeData swipeData)
        {
            if (swipeData.direction == SwipeDirection.Down)
            {
                return;
            } else if (swipeData.direction == SwipeDirection.Up)
            {
                if (swipeData.released)
                {
                    _playerMovement.Jump();
                }
            } else
            {
                LegSpreadOnSwipe(swipeData);
            }
        }

        private void LegSpreadOnSwipe(SwipeData swipeData)
        {
            _currentLegSpread -= GetLegSpreadDiff(swipeData);
            _currentLegSpread = Mathf.Clamp01(_currentLegSpread);
            _animator.SetFloat("LegAngle", _currentLegSpread);
        }

        private float GetLegSpreadDiff(SwipeData swipeData)
        {
            float swipeDist = (swipeData.end.x - swipeData.start.x);
            float truncated = swipeDist / _swipeSensitivity;
            return Mathf.Clamp(truncated, -1f, 1f);
        }
    }
}