using UnityEngine;

namespace MyOtherHalf.Tools
{
    public class KeepInScreen : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float offSet;
        private Vector2 screenBounds;

        private void Start() 
        {
            cam = cam == null ? Camera.main : cam;
            if (cam)
            {
                screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
            }        
        } 

        private void LateUpdate() 
        {
            if (cam)
            {
                Vector2 objectPosition = transform.position;

                if (Mathf.Abs(objectPosition.x)>Mathf.Abs(screenBounds.x) + offSet)
                {
                    float xValue = objectPosition.x * -1;
                    xValue = xValue > 0 ? screenBounds.x - offSet : xValue = -screenBounds.x + offSet;
                    
                    objectPosition.x = xValue;
                }

                if (Mathf.Abs(objectPosition.y)>Mathf.Abs(screenBounds.y) + offSet)
                {
                    float yValue = objectPosition.y * -1;
                    yValue = yValue > 0 ? screenBounds.y - offSet : yValue = -screenBounds.y + offSet;

                    objectPosition.y = yValue;
                }

                transform.position = objectPosition;
            }
        }
    }

}

