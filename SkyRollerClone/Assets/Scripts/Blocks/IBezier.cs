using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    public interface IBezier
    {
        void CalculatePositions();
        List<Vector3> GetPositions();
        float GetLength();
    }
}