using System.Collections.Generic;
using System.Linq;
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
        [SerializeField]
        private List<Vector3> _waypoints;
        [SerializeField]
        private float _levelLength = 0;
        private bool _hasTurns = false;

        // Return waypoints
        public List<Vector3> BuildLevel(LevelInfo levelInfo)
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
            _waypoints.Clear();
            _levelLength = 0f;

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
            //DrawSpheres();
            return _waypoints;
        }

        public float GetLevelLength()
        {
            return _levelLength;
        }

        #region Private Methods
        private GameObject ChooseRandomPrefabFromList(List<GameObject> l)
        {
            GameObject selectedPrefab = null;
            if (_hasTurns)
            {
                List<GameObject> noCurved = l.Where(x => x.GetComponent<BaseBezier>().Curved == false).ToList();
                selectedPrefab = noCurved[Random.Range(0, noCurved.Count)];
            }
            else
            {
                selectedPrefab = l[Random.Range(0, l.Count)];
            }
            if (selectedPrefab.GetComponent<BaseBezier>().Curved)
            {
                _hasTurns = true;
            }
            if (selectedPrefab == null)
            {
                Debug.LogError("No prefab selected!");
            }
            return selectedPrefab;
        }

        private void ClearPreviousLevel()
        {
            foreach (GameObject go in _instantiatedBlocks)
            {
                Destroy(go);
            }
            _instantiatedBlocks.Clear();
            _hasTurns = false;
        }

        // Returns new exit
        private Transform AddBlock(GameObject prefab, Transform exit)
        {
            if (prefab != null)
            {
                GameObject newBlockObj = Instantiate(prefab, gameObject.transform);
                Block block = newBlockObj.GetComponent<Block>();
                MatchBlocks(exit, block._enter);
                _waypoints.AddRange(block.GetPositions());
                _levelLength += block.GetLength();
                _instantiatedBlocks.Add(newBlockObj);
                return newBlockObj.GetComponent<Block>()._exit;
            } else
            {
                return exit;
            }
        }

        private void MatchBlocks(Transform exit, Transform enter)
        {
            Transform newBlock = enter.transform.parent;
            Vector3 forvardToMatch = -exit.forward;
            float rotationAngle = GetMatchRotation(forvardToMatch) - GetMatchRotation(enter.transform.forward);
            newBlock.RotateAround(enter.transform.position, Vector3.up, rotationAngle);
            Vector3 translation = exit.transform.position - enter.transform.position;
            newBlock.transform.position += translation;
        }

        private float GetMatchRotation(Vector3 v)
        {
            return Vector3.Angle(Vector3.forward, v) * Mathf.Sign(v.x);
        }

        private void DrawSpheres()
        {
            SphereCollider[] colliders = FindObjectsOfType<SphereCollider>();
            foreach (var c in colliders)
            {
                if (c.gameObject.name == "Sphere(Clone)")
                {
                    Destroy(c.gameObject);
                }
            }

            GameObject prefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            prefab.transform.localScale = Vector3.one * 0.1f;
            prefab.GetComponent<Collider>().enabled = false;
            prefab.GetComponent<Renderer>().material.color = Color.blue;

            for (int i = 0; i < _waypoints.Count; i++)
            {
                GameObject go = Instantiate(prefab, _waypoints[i], Quaternion.identity);
            }
            prefab.SetActive(false);
        }
        #endregion
    }
}