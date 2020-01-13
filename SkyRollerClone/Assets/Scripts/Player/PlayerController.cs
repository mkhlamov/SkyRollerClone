using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _animatedModel;
        [SerializeField]
        private GameObject _ragdoll;
        [SerializeField]
        private Animator _animator;
        private PlayerInputController _playerInputController;
        private PlayerMovement _playerMovement;
        [SerializeField]
        private Rigidbody _rb;
        [SerializeField]
        private List<Vector3> _waypoints;

        #region Monobehaviour
        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInputController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            _ragdoll.SetActive(false);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameWon += HandeleWin;
            GameManager.Instance.OnGameLost += HandleLose;
            GameManager.Instance.OnNotStarted += HandleNotStarted;
        }

        private void OnDisable()
        {
            if (!GameManager.IsInitialized)
            {
                return;
            }
            GameManager.Instance.OnGameWon -= HandeleWin;
            GameManager.Instance.OnGameLost -= HandleLose;
            GameManager.Instance.OnNotStarted -= HandleNotStarted;
        }
        #endregion

        #region Public Methods
        public void EnableInput()
        {
            _playerInputController.enabled = true;
        }

        public void DisableInput()
        {
            _playerInputController.enabled = true;
        }

        public void PlayRandomExcitedAnim()
        {
            int randInt = UnityEngine.Random.Range(0, _animator.runtimeAnimatorController.animationClips.Length - 2);
            _animator.SetInteger("randExcited", randInt);
            _animator.ResetTrigger("Rolling");
            _animator.SetTrigger("Excited");
        }

        public void GoToStart()
        {
            _rb.isKinematic = true;
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
            ResetLegs();
            _playerMovement.ResetCurrentWaypointAndPrevPos();
            _rb.isKinematic = false;
        }
        #endregion

        #region Private Methods
        private void CopyTransformData(Transform source, Transform dest, Vector3 velocity)
        {
            if (source.childCount != dest.childCount)
            {
                Debug.LogError("[PlayerController] source: " + source.name + " doesn't match destination: " + dest.name);
                return;
            }

            for (int i = 0; i < source.childCount; i++)
            {
                Transform sourceChild = source.GetChild(i);
                Transform destChild = dest.GetChild(i);
                destChild.position = sourceChild.position;
                destChild.rotation = sourceChild.rotation;
                Rigidbody rb = destChild.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = velocity;
                }

                CopyTransformData(sourceChild, destChild, velocity);
            }
        }

        private void ToggleDead(bool e)
        {
            if (e)
            {
                CopyTransformData(_animatedModel.transform, _ragdoll.transform, _rb.velocity + Vector3.down * 0.01f);
                ResetLegs();
            }
            _rb.isKinematic = e;
            _ragdoll.SetActive(e);
            _animatedModel.SetActive(!e);
        }

        private void HandeleWin()
        {
            DisableInput();
            PlayRandomExcitedAnim();
        }

        private void HandleLose()
        {
            ToggleDead(true);
        }

        private void HandleNotStarted()
        {
            ToggleDead(false);
            DisableInput();
            ResetLegs();
            _animator.ResetTrigger("Excited");
            _animator.SetTrigger("Rolling");
        }

        private void ResetLegs()
        {
            _animator.SetFloat("LegAngle", 0f);
            _playerInputController.ResetLegSpread();
        }
        #endregion
    }
}