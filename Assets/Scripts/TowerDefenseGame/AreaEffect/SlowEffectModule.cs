using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.AreaEffect
{
    public class SlowEffectModule : EffectModule
    {
        private float _slow;

        public override void ApplyStat(EmitterStat stat)
        {
            _slow = stat.effectData.slow;
        }

        public override void ApplyEffect(Collider2D target)
        {
            var enemyMovement = target.GetComponent<EnemyMovementModule>();

            if (enemyMovement == null) return;

            enemyMovement.TimeOffset += enemyMovement.TimeOffset * _slow;
        }

        public override void RemoveEffect(Collider2D target)
        {
            var enemyMovement = target.GetComponent<EnemyMovementModule>();

            if (enemyMovement == null) return;

            enemyMovement.ResetTimeOffset();
        }
    }
}