using UnityEngine;

namespace SkyRollerClone
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _notStartedUI;
        [SerializeField]
        private GameObject _runningUI;
        [SerializeField]
        private GameObject _winUI;
        [SerializeField]
        private GameObject _loseUI;

        private void OnEnable()
        {
            GameManager.Instance.OnNotStarted += GameNotStartedHandler;
        }

        private void OnDisable()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.OnNotStarted -= GameNotStartedHandler;
            }
        }

        private void GameNotStartedHandler()
        {

        }
    }
}