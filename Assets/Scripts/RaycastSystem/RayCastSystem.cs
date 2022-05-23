using UnityEngine;

namespace MyOtherHalf.Tools.CastMyWay
{
    public class RayCastSystem <T>
    {
        public bool _rayCastDebugMode = false;

        public T GetObjectDetection(Vector3 startPoint, Vector3 direction, float distance , LayerMask layerMask = default)
        {
            RaycastHit2D hit2D;
            DebugMode(startPoint, direction);

            hit2D = Physics2D.Raycast(startPoint, direction, distance, layerMask);
            
            if (hit2D) return hit2D.collider.gameObject.GetComponent<T>();

            return default;
        }

        public bool HitObject(Vector3 startPoint, Vector3 direction, float distance , LayerMask layerMask = default)
        {
            DebugMode(startPoint, direction);
            return Physics2D.Raycast(startPoint, direction, distance, layerMask);
        }

#if DEBUG
        private void DebugMode(Vector3 startPoint,Vector3 direction)
        {
            if (_rayCastDebugMode)
            {
                Debug.DrawRay(startPoint, direction, Color.red, 1);
            }
        }
#endif
    }
}
