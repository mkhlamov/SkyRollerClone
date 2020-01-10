using UnityEngine;

namespace SkyRollerClone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 3f;

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

        public void StartMoving()
        {
            _speed = 3f;
        }

        public void StopPlayer()
        {
            SetSpeed(0f);
        }
    }
}