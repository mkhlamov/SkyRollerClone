using UnityEngine;

namespace SkyRollerClone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _defaultSpeed = 3f;
        [SerializeField]
        private float _speed = 3f;
        private Rigidbody _rb;

        #region Monobehaviour

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + Vector3.forward * Time.fixedDeltaTime * _speed);
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

        public void StartMoving()
        {
            _speed = _defaultSpeed;
        }

        public void StopPlayer()
        {
            _rb.velocity = Vector3.zero;
            SetSpeed(0f);
        }
    }
}