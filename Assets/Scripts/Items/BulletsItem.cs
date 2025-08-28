using UnityEngine;

public class BulletsItem : CollectibleItemBase
{
    protected override void OnCollected(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            collision.SendMessage("ResetBullets");
            Runner.Despawn(Object);
        }
    }
}
