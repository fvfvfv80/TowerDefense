using UnityEngine;

public class EnemyActor : BaseActor
{
    private EnemyRole _enemyRole;


    public void Init()
    {
        var cacher = RegisterTransformComponent("Actor", transform);

        _enemyRole = cacher.GetCachedCompoent<EnemyRole>();
        _enemyRole.Init(this);
    }

    public void Setup(Transform[] wayPoints)
    {
        _enemyRole.Setup(wayPoints);
    }
}
