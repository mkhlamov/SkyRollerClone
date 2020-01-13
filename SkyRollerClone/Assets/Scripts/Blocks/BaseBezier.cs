using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public class BaseBezier : MonoBehaviour, IBezier
    {
        public bool Curved
        {
            get { return _curved; }
        }

        [SerializeField]
        protected Transform _start;
        [SerializeField]
        protected Transform _end;
        [SerializeField]
        protected int _numberOfSections = 30;
        [SerializeField]
        protected float _length;
        [SerializeField]
        protected List<Vector3> _positions = new List<Vector3>();
        [SerializeField]
        protected bool _includeEnd = false;
        [SerializeField]
        private bool _curved;

        private void OnDrawGizmos()
        {
            //DrawGizmos();
        }

        #region Public Methods
        public void CalculatePositions()
        {
            _positions.Clear();
            for (int i = 0; i < _numberOfSections; i++)
            {
                float t = i / (float)_numberOfSections;
                _positions.Add(GetBezierPoint(t));
            }
            if (_includeEnd)
            {
                _positions.Add(_end.position);
            }

            _length = CalculateLength();
        }

        public List<Vector3> GetPositions()
        {
            CalculatePositions();
            return _positions;
        }

        public float GetLength()
        {
            return _length;
        }
        #endregion

        protected void DrawGizmos()
        {
            if (_positions.Count == 0)
            {
                CalculatePositions();
            }
            for (int i = 0; i < _positions.Count - 1; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(_positions[i], _positions[i + 1]);
                Gizmos.DrawSphere(_positions[i], 0.01f);
            }
            Gizmos.DrawSphere(_positions[_positions.Count - 1], 0.01f);
        }

        protected virtual Vector3 GetBezierPoint(float t)
        {
            throw new System.NotImplementedException();
        }

        protected float CalculateLength()
        {
            float sum = 0.0f;
            for (int i = 0; i < _positions.Count - 1; i++)
            {
                sum += Vector3.Distance(_positions[i], _positions[i + 1]);
            }
            return sum;
        }
    }
}