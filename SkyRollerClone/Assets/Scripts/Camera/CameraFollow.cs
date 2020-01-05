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
        private float _smoothModifier = 10f;
        [SerializeField]
        private Vector3 _offset = new Vector3(0, 1, -2);

        void LateUpdate()
        {
            MoveToTarget();
        }

        void MoveToTarget()
        {
            Vector3 pos = _target.position + _offset;
            pos = Vector3.Lerp(transform.position, pos, _smoothModifier * Time.deltaTime);
            transform.position = pos;
        }
    }
}