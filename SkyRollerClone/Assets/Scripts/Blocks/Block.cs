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