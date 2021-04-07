using UnityEngine;

namespace Levels
{
    public class PlayerPusher : MonoBehaviour
    {
        private const float Power = 10f;

        [SerializeField] private InputHandler _inputHandler;

        private void OnEnable()
        {
            _inputHandler.PushStared += PushPlayerBodyParts;
        }
    
        private void OnDisable()
        {
            _inputHandler.PushStared -= PushPlayerBodyParts;
        }

        private void PushPlayerBodyParts(Collider2D[] results, Vector3 mousePosition)
        {
            for (int i = 0; i < results.Length; i++)
            {
                if(results[i] == null)
                    continue;;
            
                float distance = Vector2.Distance(results[i].transform.position, mousePosition);
                Vector3 direction = results[i].transform.position - mousePosition;
                direction.z = 0;
                Vector2 force = direction.normalized * Power / distance;
                results[i].attachedRigidbody.velocity = Vector2.right;
                results[i].attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
