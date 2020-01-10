using UnityEngine;

namespace SkyRollerClone {
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField]
        private GameObject _blockStartPrefab;
        [SerializeField]
        private GameObject _blockFinishPrefab;
        [SerializeField]
        private GameObject _blockEasyPrefab;
        [SerializeField]
        private GameObject _blockMediumPrefab;
        [SerializeField]
        private GameObject _blockHardPrefab;
        [SerializeField]
        private int _numberOfBlocks = 1;

        public void BuildLevel()
        {
            GameObject startBlock = Instantiate(_blockStartPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            Transform exit = startBlock.GetComponent<Block>()._exit;

            for (int i = 0; i < _numberOfBlocks; i++)
            {
                exit = AddBlock(_blockEasyPrefab, exit);
            }

            AddBlock(_blockFinishPrefab, exit);
        }

        // Returns new exit
        private Transform AddBlock(GameObject prefab, Transform exit)
        {
            GameObject newBlock = Instantiate(prefab, gameObject.transform);
            MatchBlocks(exit, newBlock.GetComponent<Block>()._enter);
            return newBlock.GetComponent<Block>()._exit;
        }

        private void MatchBlocks(Transform exit, Transform enter)
        {
            Transform newBlock = enter.transform.parent;
            Vector3 forvardToMatch = exit.forward;
            float rotationAngle = GetMatchRotation(forvardToMatch) - GetMatchRotation(exit.transform.forward);
            newBlock.RotateAround(enter.transform.position, Vector3.up, rotationAngle);
            Vector3 translation = exit.transform.position - enter.transform.position;
            newBlock.transform.position += translation;
        }

        private float GetMatchRotation(Vector3 v)
        {
            return Vector3.Angle(Vector3.forward, v) * Mathf.Sign(v.x);
        }
    }
}