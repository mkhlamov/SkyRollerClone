using System;
using UnityEngine;
using SkyRollerClone.Player;
using SkyRollerClone.Input;

namespace SkyRollerClone {
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnGameWon;
        public event Action OnGameLost;
        public event Action OnNotStarted;
        public event Action OnGameRunning;
        public event Action<int> OnLevelUpdated;

        [SerializeField]
        private Transform _startBlock;
        [SerializeField]
        private Transform _endBlock;

        private PlayerMovement _playerMovement;
        private PlayerController _playerController;
        private LevelBuilder _levelBuilder;

        private int _currentLevel = 0;
        private GameState _currentGameState = GameState.NOTSTARTED;

        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _playerController = FindObjectOfType<PlayerController>();
            _levelBuilder = FindObjectOfType<LevelBuilder>();
            
            _levelBuilder.BuildLevel();
            SetGameNotStarted();
        }

        private void OnEnable()
        {
            SwipeDetector.OnSwipe += SwipeHandler;
        }

        private void OnDisable()
        {
            SwipeDetector.OnSwipe -= SwipeHandler;
        }

        public void SetGameWon()
        {
            _currentGameState = GameState.WON;
            OnGameWon?.Invoke();
        }

        public void SetGameLost()
        {
            _currentGameState = GameState.LOST;
            OnGameLost?.Invoke();
        }

        public float GetProgress()
        {
            return (_playerMovement.gameObject.transform.position.z - _startBlock.position.z) / (_endBlock.position.z - _startBlock.position.z);
        }

        public GameState GetGameState()
        {
            return _currentGameState;
        }

        public void StartNewLevel()
        {
            SetGameNotStarted();
        }

        public int GetCurrentLevel()
        {
            return _currentLevel;
        }

        private void SetGameNotStarted()
        {
            _currentGameState = GameState.NOTSTARTED;
            _playerController.GoToStart();
            _playerMovement.StopPlayer();
            OnNotStarted?.Invoke();
        }

        private void GameRunning()
        {
            _currentGameState = GameState.RUNNING;
            _playerMovement.StartMoving();
            OnGameRunning?.Invoke();
        }

        private void SwipeHandler(SwipeData d)
        {
            if (_currentGameState == GameState.NOTSTARTED)
            {
                GameRunning();
            }
        }
    }

    public enum GameState
    {
        NOTSTARTED,
        RUNNING,
        LOST,
        WON
    }
}