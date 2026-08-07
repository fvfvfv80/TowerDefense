using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyActor : BaseActor, IEnemyModuleHost
    {

        [SerializeField]
        private Transform hudPoint;

        private TowerDefenseEnemyRolePlay _enemyRolePlay;

        public EnemyBaseModule EnemyBase { get; private set; }

        public EnemyAnimationModule EnemyAnimation { get; private set; }

        public EnemyMovementModule EnemyMovement { get; private set; }

        public EnemyHPModule EnemyHP { get; private set; }

        public Transform HUDPoint => hudPoint;


        //enemyActor는 prefab에 관련된 코드니까 prefab을 생성하는 enemyspawner를 알아도 상관없음

        private void Awake()
        {
            //init에서 실행될수도있음
            EnemyBase = GetComponent<EnemyBaseModule>();
            EnemyAnimation = GetComponent<EnemyAnimationModule>();
            EnemyMovement = GetComponent<EnemyMovementModule>();
            EnemyHP = GetComponent<EnemyHPModule>();

        }


        public void Init(TowerDefenseEnemyRolePlay enemyRolePlay)
        {
            _enemyRolePlay = enemyRolePlay;

            EnemyMovement.Init(this);
            EnemyHP.Init(this);
        }


        public void Setup(Transform[] wayPoints)
        {
            EnemyHP.Setup();
            EnemyMovement.Setup(wayPoints);

            EnemyMovement.StartMove();
        }

        #region EnemyHandle

        public void HandleHit()
        {
            EnemyAnimation.PlayHitAnimation();
        }

        public void HandleDespawn(EnemyDestroyType type)
        {
            if (type == EnemyDestroyType.Arrive)
            {
                _enemyRolePlay.OrderAttackPlayer();
            }
            else if (type == EnemyDestroyType.Kill)
            {
                _enemyRolePlay.OrderDropGold(EnemyBase.Gold);
            }

            _enemyRolePlay.OrderDespawnEnemy(this);

        }


        #endregion
    }
}