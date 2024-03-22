using Fusion;
using UnityEngine;

public abstract class CollectibleItemBase : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollected(collision);
    }

    protected abstract void OnCollected(Collider2D collision);
}
