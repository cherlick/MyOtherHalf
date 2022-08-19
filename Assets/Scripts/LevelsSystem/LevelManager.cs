using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyOtherHalf.Tools;
using UnityEngine.SceneManagement;
using System;

namespace MyOtherHalf.Core.LevelsSystem
{
    public class LevelManager : Singleton<LevelManager>
    {
        public static Action OnRestartRequest;
        public static Action OnSceneChange;
        [SerializeField] LevelsData[] allLevels;

        private Dictionary<ScenesNames, LevelsData> levels = new Dictionary<ScenesNames, LevelsData>();
        private ScenesNames currentScene;
        private ScenesNames nextLevel;
        private float currentLevelSteps;
        private LevelsTypes currentLevelType;

        public float GetCurrentLevelSteps => currentLevelSteps;
        public LevelsTypes GetCurrentLevelType => currentLevelType;

        private void Awake() 
        {
            foreach (var leveldata in allLevels)
            {
                levels.Add(leveldata.sceneName, leveldata);
            }
        }

        public void ChangeScene(ScenesNames nextSceneName)
        {
            Debug.Log($"Is {nextSceneName} a Valid scena? {IsSceneValid(nextSceneName)}");

            if (!IsSceneValid(nextSceneName)) return;

            if (nextSceneName != currentScene)
            {
                ScenesManager.UnloadScene(currentScene);
            }
            
            ScenesManager.LoadScene(nextSceneName, LoadSceneMode.Additive);

            currentScene = nextSceneName;
            OnSceneChange?.Invoke();
            if (IsSceneALevel(currentScene))
            {
                SetLevelData();
            }
            else
            {
                currentLevelType = LevelsTypes.None;
            }
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ChangeLevel()
        {
            ChangeScene(nextLevel);   
        }

        private bool IsSceneALevel(ScenesNames nextSceneName) => levels.ContainsKey(nextSceneName);

        private bool IsSceneValid(ScenesNames nextSceneName)
        {
            return SceneUtility.GetBuildIndexByScenePath(nextSceneName.ToString()) != -1;
        }

        private void SetLevelData()
        {
            nextLevel = levels[currentScene].nextSceneName;
            currentLevelSteps = levels[currentScene].maxNumberOfSteps;
            currentLevelType = levels[currentScene].levelType;
        }
    }
}
