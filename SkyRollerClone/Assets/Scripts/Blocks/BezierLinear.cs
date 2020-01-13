using UnityEngine;

namespace SkyRollerClone
{
    public class BezierLinear : BaseBezier
    {
        protected override Vector3 GetBezierPoint(float t)
        {
            return _start.position + t * (_end.position - _start.position);
        }
    }
}