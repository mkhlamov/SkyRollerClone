using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        public Transform _enter;
        [SerializeField]
        public Transform _exit;
        [SerializeField]
        public bool isFinish;
        [SerializeField]
        private IBezier _bezier;

        private void OnEnable()
        {
            _bezier = GetComponent<IBezier>();
            //_bezier.CalculatePositions();
        }

        public List<Vector3> GetPositions()
        {
            return _bezier.GetPositions();
        }

        public float GetLength()
        {
            return _bezier.GetLength();
        }

        public Transform GetFinishPos()
        {
            if (isFinish)
            {
                return GetComponentInChildren<Finish>().transform;
            } else
            {
                return null;
            }
        }
    }
}