using UnityEngine;
using MyOtherHalf.Core;

namespace MyOtherHalf.Objects
{
    public class Portal : MonoBehaviour
    {
        private void Start() 
        {
            GameManager.OnPortalStart.Invoke(this.gameObject);
            gameObject.SetActive(false);
        }
        
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Player"))
            {
                GameManager.OnLevelComplete.Invoke();
            }
        }
        
    }
}

