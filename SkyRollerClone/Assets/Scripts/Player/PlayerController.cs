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
        [SerializeField]
        private Collider[] _colliders;

        private bool _dead;

        #region Monobehaviour
        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInputController>();
            _colliders = GetComponentsInChildren<Collider>();
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

        public void ChangeCollidersActive(bool e)
        {
            foreach (var c in _colliders)
            {
                c.enabled = e;
            }
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

        private void ToggleDead()
        {
            _dead = !_dead;

            if (_dead)
            {
                Rigidbody rb = _animatedModel.GetComponent<Rigidbody>();
                Vector3 velocity = (rb != null) ? rb.velocity : (0.1f * Vector3.forward);
                CopyTransformData(_animatedModel.transform, _ragdoll.transform, velocity);
            }
            _ragdoll.SetActive(_dead);
            _animatedModel.SetActive(!_dead);
        }

        private void HandeleWin()
        {
            DisableInput();
            ChangeCollidersActive(false);
            PlayRandomExcitedAnim();
        }

        private void HandleLose()
        {
            ToggleDead();
        }

        private void HandleNotStarted()
        {
            DisableInput();
            _animator.ResetTrigger("Excited");
            _animator.SetTrigger("Rolling");
        }
        #endregion
    }
}