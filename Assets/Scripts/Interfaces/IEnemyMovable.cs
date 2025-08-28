using UnityEngine;

public interface IEnemyMovable
{
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; }
    public void MoveEnemy(Vector2 velocity);
    public void CheckForLeftOrRightFacing(Vector2 velocity);
}
