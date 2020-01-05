using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _animatedModel;
        [SerializeField]
        private GameObject _ragdoll;

        private bool _dead;

        private void Awake()
        {
            _ragdoll.SetActive(false);
        }

        public void ToggleDead()
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
    }
}