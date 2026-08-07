using Assets.Scripts.TowerDefenseGame.Enemy;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{
    public class EnemySpawnFlow : MonoBehaviour
    {

        [SerializeField]
        private TowerDefenseEnemyRolePlay enemyRole;

        [SerializeField]
        private GameObject enemyHPPrefab;

        [SerializeField]
        private Transform hpUIParent;

        [SerializeField]
        private Transform[] wayPoints; //시스템으로 갈수도있음




        public EnemyActor SpawnEnemy(GameObject enemyPrefab)
        {
            //오브젝트 풀링 가능
            GameObject clone = Instantiate(enemyPrefab);
            var enemyActor = clone.GetComponent<EnemyActor>();

            enemyActor.Init(enemyRole);
            enemyActor.Setup(wayPoints);

            SpawnEnemyHP(enemyActor);
            return enemyActor;
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
           // enemySystem.ReturnEnemy(enemyActor);
        }


    }
}


