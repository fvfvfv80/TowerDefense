using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;


        private List<EnemyActor> _enemyList = new();

        public List<EnemyActor> EnemyList => _enemyList;

 

        public EnemyActor SpawnEnemy()
        {
            GameObject clone = Instantiate(enemyPrefab);
            EnemyActor enemyActor = clone.GetComponent<EnemyActor>();

            //오브젝트풀링가능

            _enemyList.Add(enemyActor);


            return enemyActor;
        }


        public void DestroyEnemy(EnemyActor enemy)
        {
            EnemyList.Remove(enemy);
            Destroy(enemy.gameObject);
        }



    }
}


