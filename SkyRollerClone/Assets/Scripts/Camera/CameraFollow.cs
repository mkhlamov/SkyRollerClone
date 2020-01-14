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
        private float _distanceDamp = 0.1f;
        [SerializeField]
        private Vector3 _offset = new Vector3(0, 1, -2);
        [SerializeField]
        private Vector3 velocity = Vector3.one;

        [SerializeField]
        private Vector3 _middlePointOffsetFinish;
        [SerializeField]
        private Vector3 _endPointOffsetFinish;
        [SerializeField]
        private float _timeToMoveFinish = 2f;

        private float _tickFinish = 0f;
        private CameraState _cameraState = CameraState.FOLLOWPLAYER;

        #region Monobehaviour
        void LateUpdate()
        {
            if (_cameraState == CameraState.STOPPED)
            {
                return;
            }

            if (_cameraState == CameraState.FOLLOWPLAYER)
            {
                MoveToTarget();
            }
        }

        private void Update()
        {
            if (_cameraState == CameraState.STOPPED)
            {
                return;
            }

            if (_cameraState == CameraState.CURVE)
            {
                MoveByCurve();
            }
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameWon += HandeleWin;
            GameManager.Instance.OnNotStarted += HandleNotStarted;
            GameManager.Instance.OnGameLost += HandleLose;
        }

        private void OnDisable()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.OnGameWon -= HandeleWin;
                GameManager.Instance.OnNotStarted -= HandleNotStarted;
                GameManager.Instance.OnGameLost -= HandleLose;
            }
        }
        #endregion
        
        public void Follow()
        {
            _cameraState = CameraState.FOLLOWPLAYER;
        }

        public void Unfollow()
        {
            _tickFinish = 0f;
            _cameraState = CameraState.CURVE;
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

        private void HandleLose()
        {
            _cameraState = CameraState.STOPPED;
        }

        private void MoveToTarget()
        {
            Vector3 pos = _target.position + (_target.rotation * _offset);
            pos = new Vector3(pos.x, 1.7f, pos.z);
            pos = Vector3.SmoothDamp(transform.position, pos, ref velocity, _distanceDamp);
            transform.position = pos;

            transform.LookAt(_lookAtTargetFinish);
        }

        private void MoveByCurve()
        {
            if (_tickFinish < 1f)
            {
                _tickFinish += Time.deltaTime / _timeToMoveFinish;

                Vector3 p1 = Vector3.Lerp(transform.position, ConsiderRotation(_middlePointOffsetFinish), _tickFinish);
                Vector3 p2 = Vector3.Lerp(ConsiderRotation(_middlePointOffsetFinish), ConsiderRotation(_endPointOffsetFinish), _tickFinish);
                transform.position = Vector3.Lerp(p1, p2, _tickFinish);
                transform.LookAt(_lookAtTargetFinish);
            }
        }

        private Vector3 ConsiderRotation(Vector3 offset)
        {
            return _target.position + (_target.rotation * offset);
        }
        #endregion
    }

    public enum CameraState
    {
        STOPPED,
        FOLLOWPLAYER,
        CURVE
    }
}