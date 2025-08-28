using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{
    private const string PLAYER_TAG = "Player";

    protected Enemy _enemy;
    protected Transform _transform;
    protected GameObject _gameObject;

    protected List<Transform> _playersTransform = new();

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        _gameObject = gameObject;
        _transform = gameObject.transform;
        _enemy = enemy;

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag(PLAYER_TAG);

        foreach (var obj in playerObjects)
        {
            _playersTransform.Add(obj.transform);
        }
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() { }
    public virtual void DoPhisicsUpdateLogic() { }
    public virtual void DoAnimationTriggerLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
