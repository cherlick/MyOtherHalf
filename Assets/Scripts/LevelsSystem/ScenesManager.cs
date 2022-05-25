using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyOtherHalf.LevelsSystem
{
    public static class ScenesManager {
        public static void LoadScene(int index, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if(index < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(index, mode);
            }
        }

        public static void LoadScene(ScenesNames sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (IsValidScene(sceneName.ToString()))
            {
                SceneManager.LoadScene(sceneName.ToString(), mode);
            }
        }

        public static void UnloadScene(ScenesNames sceneName)
        {
            try
            {
                SceneManager.UnloadSceneAsync(sceneName.ToString());
            }
            catch (System.Exception)
            {
                Debug.LogWarning($"Unable to unload scene {sceneName}");
            }
        }

        private static bool IsValidScene(string sceneName)
        {
            int sceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);

            return sceneIndex >= 0 ? true : false;
        }

        public static void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
