using System;
using UnityEngine;

namespace SkyRollerClone.Input
{
    public class SwipeDetector : MonoBehaviour
    {
        private Vector2 _downPos;
        private Vector2 _upPos;

        [SerializeField]
        private float _minHorizontalSwipeDist = 10f;
        [SerializeField]
        private float _minVerticalSwipeDist = 30f;

        public static event Action<SwipeData> OnSwipe = delegate { };

        void Update()
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _upPos = touch.position;
                    _downPos = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    _downPos = touch.position;
                    DetectSwipe();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _downPos = touch.position;
                    DetectSwipe();
                }
            }
        }

        private void DetectSwipe()
        {
            if (IsEnoughSwipeDist())
            {
                SendSwipe(GetDirection());
                _upPos = _downPos;
            }
        }

        private SwipeDirection GetDirection()
        {
            SwipeDirection direction;
            if (IsVertical())
            {
                direction = _downPos.y - _upPos.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            } else
            {
                direction = _downPos.x - _upPos.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            }
            return direction;
        }

        private void SendSwipe(SwipeDirection direction)
        {
            SwipeData swipeData = new SwipeData()
            {
                start = _downPos,
                end = _upPos,
                direction = direction
            };
            OnSwipe?.Invoke(swipeData);
        }

        private bool IsVertical()
        {
            return VerticalDist() > HorizontalDist();
        }

        private bool IsEnoughSwipeDist()
        {
            return VerticalDist() > _minVerticalSwipeDist || HorizontalDist() > _minHorizontalSwipeDist;
        }

        private float VerticalDist()
        {
            return Math.Abs(_downPos.y - _upPos.y);
        }

        private float HorizontalDist()
        {
            return Math.Abs(_downPos.x - _upPos.x);
        }
    }

    public struct SwipeData
    {
        public Vector2 start;
        public Vector2 end;
        public SwipeDirection direction;
    }

    public enum SwipeDirection
    {
        Up, Down, Left, Right
    }
}