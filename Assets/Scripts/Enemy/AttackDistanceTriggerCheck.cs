using UnityEngine;

public class AttackDistanceTriggerCheck : MonoBehaviour
{
    private Enemy _enemy;
    private Collider2D playerCollisionRef;

    private const string PLAYER_TAG = "Player";

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            if (_enemy != null)
            {
                _enemy.IsWithinAttackDistance = true;
                playerCollisionRef = collision;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollisionRef)
        {
            if (_enemy != null)
            {
                _enemy.IsWithinAttackDistance = false;
            }
        }
    }
}
