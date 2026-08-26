using Assets.Scripts.TowerDefenseGame.Enemy;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.EnemyWave
{
    public class EnemySpawnModule : MonoBehaviour
    {

        [SerializeField]
        private GameObject enemyHPPrefab;

        [SerializeField]
        private Transform hpUIParent;

        public EnemyActor SpawnEnemy(GameObject enemyPrefab)
        {
            //오브젝트 풀링 가능
            GameObject clone = Instantiate(enemyPrefab);
            var enemyActor = clone.GetComponent<EnemyActor>();

            enemyActor.Init();
            enemyActor.Setup();

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
            enemyActor.Release();
            Destroy(enemyActor.gameObject);
        }

    }
}

