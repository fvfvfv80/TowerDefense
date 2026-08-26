using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public interface IEnemyScenario
    {
        void NotifyKilled(EnemyActor enemyActor);
        void NotifyReachedGoal(EnemyActor enemyActor);
    }

    public class EnemyActor : BaseActor, IEnemyModuleHost
    {

        [SerializeField]
        private Transform hudPoint;

        [SerializeField]
        private EnemyHPModule enemyHP;

        [SerializeField]
        private EnemyAnimationModule enemyAnimationModule;

        [SerializeField]
        private EnemyMovementModule enemyMovement;

        [SerializeField]
        private int gold = 10;

        private IEnemyScenario _enemyScenario;

        public EnemyHPModule EnemyHP => enemyHP;

        public int RewardGold => gold;

        public Transform HUDPoint => hudPoint;

        public void SetupScenario(IEnemyScenario enemyScenario)
        {
            _enemyScenario = enemyScenario;
        }

        public void SetupPath(Transform[] wayPoints)
        {
            enemyMovement.Setup(wayPoints);
        }

        public void Init()
        {
            enemyMovement.Init(this);
            enemyHP.Init(this);
        }

        public void Setup()
        {
            enemyHP.Setup();

        }

        public void StartEnemy()
        {
            enemyMovement.StartMove();
        }

        public void Release()
        {

        }

        #region IEnemyModuleHost

        public void RequestHit()
        {
            enemyAnimationModule.PlayHitAnimation();
        }

        public void RequestDespawn(EnemyDestroyType type)
        {
            if (type == EnemyDestroyType.Arrive)
            {
                _enemyScenario.NotifyReachedGoal(this);
            }
            else if (type == EnemyDestroyType.Kill)
            {
                _enemyScenario.NotifyKilled(this);
            }

        }

        #endregion




    }
}