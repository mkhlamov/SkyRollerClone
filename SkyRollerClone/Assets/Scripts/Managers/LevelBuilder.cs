using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone {
    public class LevelBuilder : MonoBehaviour
    {
        //[SerializeField]
        //private GameObject _blockStartPrefab;
        [SerializeField]
        private GameObject _blockStart;
        [SerializeField]
        private GameObject _blockFinishPrefab;
        [SerializeField]
        private List<GameObject> _blockEasyPrefabs;
        [SerializeField]
        private List<GameObject> _blockMediumPrefabs;
        [SerializeField]
        private List<GameObject> _blockHardPrefabs;
        [SerializeField]
        private List<GameObject> _instantiatedBlocks;

        // Return End collider position
        public Transform BuildLevel(LevelInfo levelInfo)
        {
            float levelInfoProbSum = levelInfo._easyBlockProb + levelInfo._mediumBlockProb + levelInfo._hardBlockProb;
            if (levelInfoProbSum != 1.0f)
            {
                Debug.Log(levelInfoProbSum);
                Debug.Log(levelInfoProbSum != 1.0f);
                Debug.LogError("[LevelBuilder] Level " + levelInfo.name + " doesn't have correct probabilities!");
                return null;
            }
            ClearPreviousLevel();
            //GameObject startBlock = Instantiate(_blockStartPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            Transform exit = _blockStart.GetComponent<Block>()._exit;

            for (int i = 0; i < levelInfo.numberOfBlocks; i++)
            {
                float rnd = Random.Range(0f, 1f);
                GameObject chosenPrefab;
                if (rnd < levelInfo._easyBlockProb)
                {
                    chosenPrefab = ChooseRandomPrefabFromList(_blockEasyPrefabs);
                } else if (levelInfo._easyBlockProb < rnd && rnd < (levelInfo._easyBlockProb + levelInfo._mediumBlockProb))
                {
                    chosenPrefab = ChooseRandomPrefabFromList(_blockMediumPrefabs);
                } else
                {
                    chosenPrefab = ChooseRandomPrefabFromList(_blockHardPrefabs);
                }
                exit = AddBlock(chosenPrefab, exit);
            }

            AddBlock(_blockFinishPrefab, exit);
            return _instantiatedBlocks[_instantiatedBlocks.Count - 1].GetComponent<Block>().GetFinishPos();
        }

        #region Private Methods
        private GameObject ChooseRandomPrefabFromList(List<GameObject> l)
        {
            return l[Random.Range(0, l.Count)];
        }

        private void ClearPreviousLevel()
        {
            foreach (GameObject go in _instantiatedBlocks)
            {
                Destroy(go);
            }
            _instantiatedBlocks.Clear();
        }

        // Returns new exit
        private Transform AddBlock(GameObject prefab, Transform exit)
        {
            GameObject newBlock = Instantiate(prefab, gameObject.transform);
            MatchBlocks(exit, newBlock.GetComponent<Block>()._enter);
            _instantiatedBlocks.Add(newBlock);
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
        #endregion
    }
}