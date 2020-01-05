using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _velocity = 20f;

        #region Monobehaviour
        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _velocity);
        }
        #endregion
    }
}