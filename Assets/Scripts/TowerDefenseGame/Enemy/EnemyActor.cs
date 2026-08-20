using System;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyActor : BaseActor, IEnemyModuleHost
    {

        [SerializeField]
        private Transform hudPoint;

        private EnemyContext _enemyContext;

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

        public void SetupContext(EnemyContext enemyContext)
        {
            _enemyContext = enemyContext;
        }

        public void SetupPath(Transform[] wayPoints)
        {
            EnemyMovement.Setup(wayPoints);
        }

        public void Init()
        {
            EnemyMovement.Init(this);
            EnemyHP.Init(this);
        }


        public void Setup()
        {
            EnemyHP.Setup();
            
        }

        public void StartEnemy()
        {
            EnemyMovement.StartMove();
        }

        public void ReleaseContext()
        {
            _enemyContext.Release(this);
        }

        #region EnemyHandle

        public void RequestHit()
        {
            EnemyAnimation.PlayHitAnimation();
        }

        public void RequestDespawn(EnemyDestroyType type)
        {
            if (type == EnemyDestroyType.Arrive)
            {
                _enemyContext.NotifyReachGoal(this);
            }
            else if (type == EnemyDestroyType.Kill)
            {
                _enemyContext.NotifyKilled(this);
            }

        }




        #endregion
    }
}