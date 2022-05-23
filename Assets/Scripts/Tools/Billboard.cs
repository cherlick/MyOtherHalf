using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyOtherHalf.Tools.UI
{
    public class Billboard : MonoBehaviour
    {
        public Transform cam;
        public bool useMainCamara;
        
        private void Start() 
        {
            cam = useMainCamara ? Camera.main.transform : cam;
        }
        private void LateUpdate() 
        {
            if (cam)
            {
                transform.LookAt(transform.position + cam.forward);
            }
        }
    }
}

