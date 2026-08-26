using Assets.Scripts.TowerDefenseGame.Enemy;
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
            //모듈중에서 TimeOffset이 존재하거나 늦출 수 있는 존재여야함 IXXModule로 받아서 처리
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