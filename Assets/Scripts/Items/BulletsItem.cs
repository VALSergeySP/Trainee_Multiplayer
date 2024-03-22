using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsItem : CollectibleItemBase
{
    protected override void OnCollected(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("ResetBullets");
            Runner.Despawn(Object);
        }
    }
}
