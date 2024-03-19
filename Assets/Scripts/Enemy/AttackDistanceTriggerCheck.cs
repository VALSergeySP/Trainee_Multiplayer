using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDistanceTriggerCheck : MonoBehaviour
{
    private Enemy _enemy;
    private Collider2D playerCollisionRef;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
