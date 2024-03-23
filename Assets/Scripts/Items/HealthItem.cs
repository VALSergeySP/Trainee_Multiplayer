using UnityEngine;

public class HealthItem : CollectibleItemBase
{
    [SerializeField] private int _healthHealAmount = 20;

    protected override void OnCollected(Collider2D collision)
    {
        if(collision.CompareTag(PLAYER_TAG))
        {
            collision.SendMessage("Healing", _healthHealAmount);
            Runner.Despawn(Object);
        }
    }
}
