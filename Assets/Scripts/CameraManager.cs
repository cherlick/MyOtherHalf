using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyOtherHalf.LevelsSystem;
using System;

namespace MyOtherHalf.CameraSystem
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float puzzleCameraSize = 5;
        [SerializeField] private float battleCameraSize = 8;
        private Camera cam;
        private LevelsTypes currentLevelType;

        private void Awake() 
        {
            cam = GetComponent<Camera>();
            SetupCameraForLevelType();
        }

        private void SetupCameraForLevelType()
        {
            cam.orthographicSize = currentLevelType == LevelsTypes.Battle 
                ? battleCameraSize : puzzleCameraSize;
        }
    }
}

