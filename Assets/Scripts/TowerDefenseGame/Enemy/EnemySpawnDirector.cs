using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    //이번씬 또는 슈팅겜의 에네미 스폰 디렉터
    public class EnemySpawnDirector : MonoBehaviour
    {
        [SerializeField]
        private EnemySystem enemySystem;

        [SerializeField]
        private TowerDefenseFlow towerDefenseFlow;

        [SerializeField]
        private GameObject enemyHPPrefab;

        [SerializeField]
        private Transform hpUIParent;

        [SerializeField]
        private float spawnTime;

        [SerializeField]
        private Transform[] wayPoints;


        private void Awake()
        {
            StartCoroutine(nameof(SpawnEnemy));
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {

                var enemyActor = enemySystem.SpawnEnemyActor();

                enemyActor.Init(enemySystem,towerDefenseFlow);
                enemyActor.Setup(wayPoints);

                SpawnEnemyHP(enemyActor);

                yield return new WaitForSeconds(spawnTime);
            }
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

    }
}