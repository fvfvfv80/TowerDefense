using System;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyActor : BaseActor, IEnemyModuleHost
    {

        [SerializeField]
        private Transform hudPoint;

        [SerializeField]
        private int gold = 10;

        public EnemyAnimationModule EnemyAnimation { get; private set; }

        public EnemyMovementModule EnemyMovement { get; private set; }

        public EnemyHPModule EnemyHP { get; private set; }

        private EnemyRoleFlow _enemyRole;


        public int RewardGold => gold;


        public Transform HUDPoint => hudPoint;


        //enemyActor는 prefab에 관련된 코드니까 prefab을 생성하는 enemyspawner를 알아도 상관없음

        private void Awake()
        {
            //init에서 실행될수도있음
            EnemyAnimation = GetComponent<EnemyAnimationModule>();
            EnemyMovement = GetComponent<EnemyMovementModule>();
            EnemyHP = GetComponent<EnemyHPModule>();

        }

        public void SetupContext(EnemyRoleFlow enemyRole)
        {
            _enemyRole = enemyRole;
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

        public void Release()
        {

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
                _enemyRole.NotifyReachGoal(this);
            }
            else if (type == EnemyDestroyType.Kill)
            {
                _enemyRole.NotifyKilled(this);
            }

        }




        #endregion
    }
}