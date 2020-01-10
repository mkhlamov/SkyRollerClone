using UnityEngine;

namespace SkyRollerClone.UI
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
            GameManager.Instance.OnNotStarted   += GameNotStartedHandler;
            GameManager.Instance.OnGameRunning  += GameRunningHandler;
            GameManager.Instance.OnGameWon      += GameWonHandler;
            GameManager.Instance.OnGameLost     += GameLostHandler;
        }

        private void OnDisable()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.OnNotStarted   -= GameNotStartedHandler;
                GameManager.Instance.OnGameRunning  -= GameRunningHandler;
                GameManager.Instance.OnGameWon      -= GameWonHandler;
                GameManager.Instance.OnGameLost     -= GameLostHandler;
            }
        }

        private void GameNotStartedHandler()
        {
            _notStartedUI.SetActive(true);
            _runningUI.SetActive(false);
            _winUI.SetActive(false);
            _loseUI.SetActive(false);
        }

        private void GameRunningHandler()
        {
            _notStartedUI.SetActive(false);
            _runningUI.SetActive(true);
            _winUI.SetActive(false);
            _loseUI.SetActive(false);
        }

        private void GameWonHandler()
        {
            _notStartedUI.SetActive(false);
            _runningUI.SetActive(true);
            _winUI.SetActive(true);
            _loseUI.SetActive(false);
        }

        private void GameLostHandler()
        {
            _notStartedUI.SetActive(false);
            _runningUI.SetActive(true);
            _winUI.SetActive(false);
            _loseUI.SetActive(true);
        }
    }
}