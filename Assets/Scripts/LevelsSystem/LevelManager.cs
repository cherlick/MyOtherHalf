using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyOtherHalf.Tools;
using UnityEngine.SceneManagement;

namespace MyOtherHalf.LevelsSystem
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] LevelsData[] allLevels;

        private Dictionary<ScenesNames, LevelsData> levels = new Dictionary<ScenesNames, LevelsData>();
        private ScenesNames currentScene;
        private ScenesNames nextLevel;

        private void Awake() 
        {
            foreach (var leveldata in allLevels)
            {
                levels.Add(leveldata.sceneName, leveldata);
            }
        }

        private void OnEnable() 
        {
            
        }

        public void ChangeScene(ScenesNames nextScene)
        {
            ScenesManager.LoadScene(nextScene, LoadSceneMode.Additive);
            ScenesManager.UnloadScene(currentScene);
        }
    }
}
