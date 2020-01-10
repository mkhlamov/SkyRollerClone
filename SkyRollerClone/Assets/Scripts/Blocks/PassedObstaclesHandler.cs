
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class PassedObstaclesHandler : MonoBehaviour
    {
        [SerializeField]
        private Material _defaultMaterial;
        [SerializeField]
        private Material _passedMaterial;
        private List<Renderer> _renderers;
        private AudioSource _audioSource;

        #region Monobehaviour
        private void Start()
        {
            Init();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (Renderer r in _renderers)
                {
                    r.material = _passedMaterial;
                }
                _audioSource.Play();
            }
        }
        #endregion

        private void Init()
        {
            _renderers = new List<Renderer>();
            _audioSource = GetComponent<AudioSource>();
            foreach (Transform t in transform)
            {
                Renderer r = t.gameObject.GetComponent<Renderer>();
                if (r != null)
                {
                    _renderers.Add(r);
                    r.material = _defaultMaterial;
                }
            }
        }
    }
}