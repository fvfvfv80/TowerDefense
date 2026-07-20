using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyActor : BaseActor, IEnemyHandler
    {

        [SerializeField]
        private Transform hudPoint;


        private EnemySystem _enemySystem;

        private TowerDefenseFlow _towerDefenseFlow;

        //private SimpleEnemyBehavior _simpleEnemyRole; //점차 enemyactor의 IenemyHanlder 규모가 커지거나 다양해질 것 같으면 분리

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



        public void Init(EnemySystem enemySystem,TowerDefenseFlow towerDefenseFlow)
        {
            _enemySystem = enemySystem;
            _towerDefenseFlow = towerDefenseFlow;

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

        public void Hit()
        {
            EnemyAnimation.PlayHitAnimation();
        }

        public void Despawn(EnemyDestroyType type)
        {
            if(type == EnemyDestroyType.Arrive)
            {
                _towerDefenseFlow.NotifyEnemyGoal();
            }
            else if(type == EnemyDestroyType.Kill)
            {
                _towerDefenseFlow.AddPlayerGold(EnemyBase.Gold);
            }

            _enemySystem.DespawnEnemy(this);
        }

        public void ReachGoal()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}