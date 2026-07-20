
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemySystem:MonoBehaviour
    {

        private EnemySpawner _enemySpawner;

        public List<EnemyActor> EnemyList => _enemySpawner.EnemyList;


        //sceneroot가 있다면 거기서 init 될 예정
        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _enemySpawner = GetComponent<EnemySpawner>();
        }

        public EnemyActor SpawnEnemyActor()
        {
            var enemyActor = _enemySpawner.SpawnEnemy();


            return enemyActor;
        }

        public void DespawnEnemy(EnemyActor enemyActor)
        {
            _enemySpawner.DestroyEnemy(enemyActor);
        }
    }
}
