using System;
using System.Collections.Generic;
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
        public Transform _startPos;
        [SerializeField]
        public Transform _endPos;
        [SerializeField]
        private List<LevelInfo> _levelInfos;

        private PlayerMovement _playerMovement;
        private PlayerController _playerController;
        private LevelBuilder _levelBuilder;

        [SerializeField]
        private int _currentLevel = 0;
        private GameState _currentGameState = GameState.NOTSTARTED;

        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _playerController = FindObjectOfType<PlayerController>();
            _levelBuilder = FindObjectOfType<LevelBuilder>();

            BuildLevel();
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
            return (_playerMovement.gameObject.transform.position.z) / (_endPos.position.z);
        }

        public GameState GetGameState()
        {
            return _currentGameState;
        }

        public void StartNewLevel()
        {
            if (_currentLevel == _levelInfos.Count - 1)
            {
                _currentLevel = 0;
            }
            else
            {
                _currentLevel += 1;
            }
            OnLevelUpdated?.Invoke(_currentLevel);

            BuildLevel();
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

        private bool BuildLevel()
        {
            _endPos = _levelBuilder.BuildLevel(_levelInfos[_currentLevel]);
            if (_endPos != null)
            {
                Debug.Log("Level " + _currentLevel + " built");
            }
            else
            {
                Debug.LogError("[GameManager] Couldn't biuld level " + (_currentLevel));
            }
            return _endPos != null;
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