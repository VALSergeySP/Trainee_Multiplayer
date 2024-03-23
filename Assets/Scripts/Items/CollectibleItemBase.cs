using Fusion;
using UnityEngine;

public abstract class CollectibleItemBase : NetworkBehaviour
{
    protected const string PLAYER_TAG = "Player";


    [SerializeField] private float _itemDespawnTime = 20f;
    [Networked] private TickTimer Life { get; set; }

    public override void Spawned()
    {
        Life = TickTimer.CreateFromSeconds(Runner, _itemDespawnTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (Life.Expired(Runner))
            Runner.Despawn(Object);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollected(collision);
    }

    protected abstract void OnCollected(Collider2D collision);
}
