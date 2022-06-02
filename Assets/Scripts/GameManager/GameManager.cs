using System;
using UnityEngine;
using MyOtherHalf.Characters;
using MyOtherHalf.Tools;
using MyOtherHalf.Core.UI;
using MyOtherHalf.Core.CameraSystem;
using MyOtherHalf.Core.LevelsSystem;

namespace MyOtherHalf.Core
{
    public class GameManager : Singleton<GameManager>
    {
        #region Events

        public static Action<CharacterData> OnMergeSelection;
        public static Action OnEnemyKilled;
        public static Action<float> OnStepsUpdated;

        #endregion

        #region Private variables
        [SerializeField] private GameObject fullHeart;

        private GameState currentGameState;
        private GameState previousGameState;
        private LevelManager levelManager;
        private float puzzleModeStepsCount;

        #endregion

        #region Properties
        
        public GameState GetCurrentGameState => currentGameState;
        public GameState GetPreviousGameState => previousGameState;

        #endregion

        #region Unity Methods
        private void Awake() 
        {
            currentGameState = GameState.StartMenu;
            ChangeState(GameState.StartMenu);
            levelManager = LevelManager.Instance;
            levelManager.ChangeScene(ScenesNames.StartScene);
        }

        private void OnEnable() 
        {
            HalfPartController.OnHalfCollision += Merge;
            HalfPartController.OnStepDone += StepUpdate;
            LevelManager.OnLevelChange += LevelChangeUpdate;
            OnMergeSelection += MergeSelection;
        }

        private void OnDisable() 
        {
            HalfPartController.OnHalfCollision -= Merge;
            HalfPartController.OnStepDone -= StepUpdate;
            LevelManager.OnLevelChange -= LevelChangeUpdate;
            OnMergeSelection -= MergeSelection;
        }

        #endregion

        #region Pivate Methods

        private void StepUpdate()
        {
            puzzleModeStepsCount += 0.5f;
            OnStepsUpdated?.Invoke(puzzleModeStepsCount);
            CheckForLosingCondition();
        }

        private void CheckForLosingCondition()
        {
            if (puzzleModeStepsCount >= 0)
            {
                LevelManager.OnRestartRequest?.Invoke();
            }
        }

        private void Merge(Vector2 position)
        {
            fullHeart.transform.position = position;
            UIManager.OnUIMergePanelUpdate?.Invoke(true, false);
        }

        private void MergeSelection(CharacterData newData)
        {
            fullHeart.GetComponent<PlayerController>().SetData = newData;
            UIManager.OnUIMergePanelUpdate?.Invoke(false,true);
        }

        private void LevelChangeUpdate()
        {
            if (levelManager.GetCurrentLevelType == LevelsTypes.Puzzles)
            {
                puzzleModeStepsCount = levelManager.GetCurrentLevelSteps;
                ChangeState(GameState.PuzzleMode);
            }
            if (levelManager.GetCurrentLevelType == LevelsTypes.Battle)
            {
                ChangeState(GameState.BattleMode);
            }
        }

        private void ChangeState(GameState newState)
        {
            previousGameState = currentGameState;
            currentGameState = newState;
            UIManager.OnUIChangeMode?.Invoke(currentGameState);
            CameraManager.Instance.ChangeCameraState(currentGameState);
        }

        #endregion

    }
}

