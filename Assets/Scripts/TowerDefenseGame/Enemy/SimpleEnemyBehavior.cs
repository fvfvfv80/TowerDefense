using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    
    public class SimpleEnemyBehavior : IEnemyHandler
    {

        private readonly EnemyBaseModule _enemyBase;                     //지금은 다 분리했지만 분리될필요없다고 생각되는 짬통 모듈이 필요할 수 도있음 EnemyBaseModule
        private readonly EnemyMovementModule _enemyMovement;
        private readonly EnemyAnimationModule _enemyAnimation;
        private readonly EnemyHPModule _enemyHP;



        public SimpleEnemyBehavior(EnemyBaseModule enemyBase, EnemyMovementModule enemyMovement, EnemyHPModule enemyHP, EnemyAnimationModule enemyAnimation)
        {
            _enemyBase = enemyBase;
            _enemyMovement = enemyMovement;
            _enemyHP = enemyHP;
            _enemyAnimation = enemyAnimation;

        }

        public void Setup()
        {

        }

        public void TakeDamage()
        {

        }


        public void Despawn(EnemyDestroyType tpye)
        {
            //_enemyBase.KillEnemy();
        }

        public void Hit()
        {
            _enemyAnimation.PlayHitAnimation();
        }

        public void ReachGoal()
        {
            throw new System.NotImplementedException();
        }
    }
}
