using UnityEngine;

namespace SkyRollerClone
{
    public class BezierQuadratic : BaseBezier
    {
        [SerializeField]
        private Transform _middle;
        protected override Vector3 GetBezierPoint(float t)
        {
            // (1-t)^2 * start + 2t(1-t)*middle + t^2*end
            float b0 = (1 - t) * (1 - t);
            float b1 = 2 * t * (1 - t);
            float b2 = t * t;
            return b0 * _start.position + b1 * _middle.position + b2 * _end.position;
        }
    }
}