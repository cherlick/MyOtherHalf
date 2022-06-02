using UnityEngine;
using MyOtherHalf.Tools;

namespace MyOtherHalf.Core.CameraSystem
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private float puzzleCameraSize = 5;
        [SerializeField] private float battleCameraSize = 8;
        private Camera cam;

        private void Awake() 
        {
            cam = GetComponent<Camera>();
        }

        public void ChangeCameraState(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.PuzzleMode:
                    cam.orthographicSize = puzzleCameraSize;
                break;

                case GameState.BattleMode:
                    cam.orthographicSize = battleCameraSize;
                break;
            }
        }
    }
}

