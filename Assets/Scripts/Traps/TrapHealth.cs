using Player;
using UnityEngine;

namespace Traps
{
  public class TrapHealth : MonoBehaviour
  {
    private const int MaxHealth = 3;

    [SerializeField] protected int CurrentHealth = MaxHealth;
  
    protected bool IsCollided;
  
    private void OnCollisionEnter2D(Collision2D other)
    {
      if(IsCollided)
        return;
    
      if (other.gameObject.TryGetComponent(out PlayerBodyPart playerBodyPart))
      {
        IsCollided = true;
        TakeDamage();
      }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
      if(!IsCollided)
        return;
    
      if (other.gameObject.TryGetComponent(out PlayerBodyPart playerBodyPart))
      {
        IsCollided = false;
      }
    }

    protected virtual void TakeDamage()
    {
      if (CurrentHealth > 0)
      {
        CurrentHealth--;
      }
      else
      {
        Die();
      }
    }

    protected virtual void Die()
    {
      Destroy(gameObject);
    }
  }
}
