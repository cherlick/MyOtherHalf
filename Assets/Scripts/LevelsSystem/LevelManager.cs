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
        public static Action OnLevelChange;
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
            ScenesManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
            ScenesManager.UnloadScene(currentScene);
            currentScene = nextSceneName;
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ChangeLevel()
        {
            if (levels.ContainsKey(nextLevel))
            {
                ChangeScene(nextLevel);
                nextLevel = levels[currentScene].nextSceneName;
                currentLevelSteps = levels[currentScene].maxNumberOfSteps;
                currentLevelType = levels[currentScene].levelType;
                OnLevelChange?.Invoke();
            }
            else
            {
                currentLevelType = LevelsTypes.None;
            }
        }
    }
}
