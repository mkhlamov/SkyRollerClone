using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private Transform _lookAtTargetFinish;
        [SerializeField]
        private float _smoothModifier = 10f;
        [SerializeField]
        private Vector3 _offset = new Vector3(0, 1, -2);
        [SerializeField]
        private Vector3 _middlePointOffsetFinish;
        [SerializeField]
        private Vector3 _endPointOffsetFinish;
        [SerializeField]
        private float _timeToMoveFinish = 2f;

        private bool needToFollow = true;
        private float _tickFinish = 0f;

        #region Monobehaviour
        void LateUpdate()
        {
            if (needToFollow)
            {
                MoveToTarget();
            }
        }

        private void Update()
        {
            if (!needToFollow)
            {
                MoveByCurve();
            }
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameWon += HandeleWin;
            GameManager.Instance.OnNotStarted += HandleNotStarted;
        }

        private void OnDisable()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.OnGameWon -= HandeleWin;
                GameManager.Instance.OnNotStarted -= HandleNotStarted;
            }
        }
        #endregion
        
        public void Follow()
        {
            needToFollow = true;
        }

        public void Unfollow()
        {
            _tickFinish = 0f;
            needToFollow = false;
        }

        #region Private Methods
        private void HandeleWin()
        {
            Unfollow();
        }

        private void HandleNotStarted()
        {
            gameObject.transform.rotation = Quaternion.identity;
            Follow();
        }

        private void MoveToTarget()
        {
            Vector3 pos = _target.position + _offset;
            pos = Vector3.Lerp(transform.position, pos, _smoothModifier * Time.deltaTime);
            transform.position = pos;
        }

        private void MoveByCurve()
        {
            if (_tickFinish < 1f)
            {
                _tickFinish += Time.deltaTime / _timeToMoveFinish;

                Vector3 p1 = Vector3.Lerp(transform.position, _target.position + _middlePointOffsetFinish, _tickFinish);
                Vector3 p2 = Vector3.Lerp(_target.position + _middlePointOffsetFinish, _target.position + _endPointOffsetFinish, _tickFinish);
                transform.position = Vector3.Lerp(p1, p2, _tickFinish);
                transform.LookAt(_lookAtTargetFinish);
            }
        }
        #endregion
    }
}