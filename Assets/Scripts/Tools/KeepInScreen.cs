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
                Vector3 viewPosition = transform.position;

                if (Mathf.Abs(viewPosition.x)>Mathf.Abs(screenBounds.x) + offSet)
                {
                    transform.position = new Vector2(viewPosition.x * -1 , viewPosition.y);
                }

                if (Mathf.Abs(viewPosition.y)>Mathf.Abs(screenBounds.y) + offSet)
                {
                    transform.position = new Vector2(viewPosition.x, viewPosition.y * -1);
                }
            }
        }
    }

}

