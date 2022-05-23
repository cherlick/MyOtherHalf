using System.Collections;
using MyOtherHalf.Tools.CastMyWay;
using UnityEngine;
using MyOtherHalf.Pooling;

namespace MyOtherHalf.Characters
{
    public class BaseCharacterController : MonoBehaviour
    {
        [SerializeField] protected new Rigidbody2D rigidbody2D; 
        private PoolingSystem poolingSystem;
        private RayCastSystem<Transform> _rayCast = new RayCastSystem<Transform>();
        
        protected void StepsMove(Vector2 direction)
        {
            Vector2 nextPosition = (Vector2)transform.position + direction;

            if (!_rayCast.HitObject(transform.position, direction,  1f, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                rigidbody2D.MovePosition(nextPosition);
            }
        }

        protected void Movement(Vector2 direction, float moveSpeed)
        {
            direction = direction * moveSpeed;
            rigidbody2D.velocity = direction;
        }

        protected void FireProjectiles(Vector2 direction, string projectileName)
        {
            Debug.Log($"Pew Pew {direction}");
            /*GameObject newBullet = poolingSystem.GetObjectFromPool(projectileName);

            newBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            newBullet.SetActive(true);*/
        }


    }
}

