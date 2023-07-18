using UnityEngine;

namespace Vertigo.Minigames
{
    public class TargetPractice : MonoBehaviour
    {
        [SerializeField] private Collider _targetCollider;


        private void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {
                //if(collision.gameObject.get)
            }
        }
    }
}
