using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemySpawnFlow : MonoBehaviour
    {
        [SerializeField]
        private EnemySystem enemySystem;

        [SerializeField]
        private TowerDefenseEnemyRole enemyRole;

        [SerializeField]
        private GameObject enemyHPPrefab;

        [SerializeField]
        private Transform hpUIParent;

        [SerializeField]
        private Transform[] wayPoints; //시스템으로 갈수도있음




        public EnemyActor SpawnEnemy(GameObject enemyPrefab)
        {
            var enemyActor = enemySystem.GetEnemy(enemyPrefab);

            enemyActor.Init(enemyRole);
            enemyActor.Setup(wayPoints);

            SpawnEnemyHP(enemyActor);
            return enemyActor;
        }


        public void DestroyEnemy(EnemyActor enemy)
        {
            enemySystem.
            EnemyList.Remove(enemy);
            Destroy(enemy.gameObject);
        }

        private void SpawnEnemyHP(EnemyActor enemyActor)
        {
            var clone = Instantiate(enemyHPPrefab);

            clone.transform.SetParent(hpUIParent);

            clone.transform.localScale = Vector3.one;


            //UI 바인딩
            clone.GetComponent<FollowTargetUI>().SetTarget(enemyActor.HUDPoint);

            clone.GetComponent<EnemyHPViewer>().Setup(enemyActor.EnemyHP);
        }

        public void DespawnEnemy(EnemyActor enemyActor)
        {
            enemySystem.ReturnEnemy(enemyActor);
        }


    }
}


