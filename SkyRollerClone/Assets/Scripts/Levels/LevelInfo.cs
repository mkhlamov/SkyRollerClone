using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "LevelInfo", order = 51)]
    public class LevelInfo : ScriptableObject
    {
        public int numberOfBlocks;
        public float _easyBlockProb;
        public float _mediumBlockProb;
        public float _hardBlockProb;
    }
}