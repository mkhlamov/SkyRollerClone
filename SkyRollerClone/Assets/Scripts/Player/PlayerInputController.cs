﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyRollerClone.Input;

namespace SkyRollerClone
{
    public class PlayerInputController : MonoBehaviour
    {
        private float _currentLegSpread = 0f;
        [SerializeField]
        private Animator _animator;
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
                _currentLegSpread -= GetLegSpreadDiff(swipeData);
                _currentLegSpread = Mathf.Clamp01(_currentLegSpread);
                Debug.Log("LegSpreadDiff = " + GetLegSpreadDiff(swipeData));
                Debug.Log("_currentLegSpread = " + _currentLegSpread);
                _animator.SetFloat("LegAngle", _currentLegSpread);
            }
        }

        private float GetLegSpreadDiff(SwipeData swipeData)
        {
            float swipeDist = (swipeData.end.x - swipeData.start.x);
            Debug.Log("swipeDist = " + swipeDist);
            float truncated = swipeDist / 100f;
            Debug.Log("truncated = " + truncated);
            return Mathf.Clamp(truncated, -1f, 1f);
        }
    }
}