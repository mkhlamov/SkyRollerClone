using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyRollerClone.Input;

namespace SkyRollerClone.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private float _currentLegSpread = 0f;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private float _swipeSensitivity = 100f;
        // Start is called before the first frame update
        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            SwipeDetector.OnSwipe += LegSpreadOnSwipe;
        }

        private void OnDisable()
        {
            SwipeDetector.OnSwipe -= LegSpreadOnSwipe;
        }

        private void LegSpreadOnSwipe(SwipeData swipeData)
        {
            if (swipeData.direction == SwipeDirection.Up || swipeData.direction == SwipeDirection.Down)
            {
                return;
            } else
            {
                Debug.Log("LegSpreadDiff = " + GetLegSpreadDiff(swipeData));
                Debug.Log("_currentLegSpread = " + _currentLegSpread);
                Debug.Log("---------------------");
                _currentLegSpread -= GetLegSpreadDiff(swipeData);
                _currentLegSpread = Mathf.Clamp01(_currentLegSpread);
                _animator.SetFloat("LegAngle", _currentLegSpread);
            }
        }

        private float GetLegSpreadDiff(SwipeData swipeData)
        {
            float swipeDist = (swipeData.end.x - swipeData.start.x);
            float truncated = swipeDist / _swipeSensitivity;
            return Mathf.Clamp(truncated, -1f, 1f);
        }
    }
}