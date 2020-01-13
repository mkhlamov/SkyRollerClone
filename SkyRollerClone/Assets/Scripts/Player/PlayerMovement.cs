using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _defaultSpeed = 3f;
        [SerializeField]
        private float _speed = 3f;
        [SerializeField]
        private Transform _groundCheck;
        [SerializeField]
        private float _groundDistance = 0.1f;
        [SerializeField]
        private float _jumpHeight = 0.1f;
        [SerializeField]
        private LayerMask _canJumpMask;
        [SerializeField]
        private bool _canJump = true;
        [SerializeField]
        private LayerMask _noJumpMask;
        [SerializeField]
        private bool _noJumpBlock = true;
        private Rigidbody _rb;
        [SerializeField]
        private List<Vector3> _waypoints = new List<Vector3>();
        [SerializeField]
        private int _currentWaypointTarget = 0;
        [SerializeField]
        private float _passedDist = 0;
        [SerializeField]
        private Vector3 _prevPos;

        #region Monobehaviour

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            
            if (_waypoints.Count == 0)
            {
                return;
            }
            if (_speed > 0f)
            {
                //_rb.MovePosition(_rb.position + Vector3.forward * Time.fixedDeltaTime * _speed);
                if (_currentWaypointTarget < _waypoints.Count)
                {
                    // if between wayopints where we can jump
                    if (_noJumpBlock)
                    {
                        Vector3 diff = (_waypoints[_currentWaypointTarget] - _rb.position).normalized;
                        _rb.MovePosition(_rb.position + diff * _speed * Time.fixedDeltaTime);
                        transform.LookAt(_waypoints[_currentWaypointTarget]);
                        //_passedDist += diff.magnitude;

                        Debug.Log(_currentWaypointTarget);
                        Debug.Log((Vector3.Distance(_rb.position, _waypoints[_currentWaypointTarget])));
                        Debug.Log("--------------------------");

                        
                    } else
                    {
                        _rb.MovePosition(_rb.position + transform.forward * Time.fixedDeltaTime * _speed);
                    }

                    if (Vector3.Distance(new Vector3(_rb.position.x, _waypoints[_currentWaypointTarget].y, _rb.position.z), _waypoints[_currentWaypointTarget]) < 0.1f)
                    {
                        _currentWaypointTarget += 1;
                    }
                }
            }
        }

        private void LateUpdate()
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }

        private void Update()
        {
            //if (_currentWaypointTarget < _waypoints.Count)
            //{
            //    transform.LookAt(_waypoints[_currentWaypointTarget]);
            //}
            _canJump = Physics.CheckSphere(_groundCheck.position, _groundDistance, _canJumpMask, QueryTriggerInteraction.Ignore);
            Debug.DrawRay(transform.position + Vector3.up * 0.1f, -transform.up);
            _noJumpBlock = Physics.Raycast(transform.position + Vector3.up * 0.1f, -transform.up, 1.0f, _noJumpMask, QueryTriggerInteraction.Ignore);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameWon += StopPlayer;
            GameManager.Instance.OnLevelBuilt += HandleLevelBuilt;
        }

        private void OnDisable()
        {
            if (!GameManager.IsInitialized)
            {
                return;
            }
            GameManager.Instance.OnGameWon -= StopPlayer;
            GameManager.Instance.OnLevelBuilt -= HandleLevelBuilt;
        }
        #endregion

        public void SetSpeed(float speed)
        {
            // Lerp here?
            _speed = speed;
        }

        public void StartMoving()
        {
            _speed = _defaultSpeed;
        }

        public void StopPlayer()
        {
            _rb.velocity = Vector3.zero;
            SetSpeed(0f);
        }

        public void Jump()
        {
            if (_canJump)
            {
                _rb.AddForce(Vector3.up * Mathf.Sqrt(-_jumpHeight * Physics.gravity.y), ForceMode.VelocityChange);
            }
        }

        public float GetPassedDist()
        {
            return _passedDist;
        }

        public void ResetCurrentWaypoint()
        {
            _currentWaypointTarget = 0;
        }

        private void HandleLevelBuilt(List<Vector3> l)
        {
            _waypoints = l;
            _passedDist = 0f;
        }
    }
}