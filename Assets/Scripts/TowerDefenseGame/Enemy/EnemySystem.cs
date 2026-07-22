
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemySystem:MonoBehaviour
    {


        private List<EnemyActor> _enemyActorList = new(); //TODO: 오브젝트 풀화시키기 

        public List<EnemyActor> EnemyList => _enemyActorList; //EnemyActor를 그대로 전달해주기보단 인터페이스 형태로 기억하다 전달해줄 확률 높음



        //sceneroot가 있다면 거기서 init 될 예정
        private void Awake()
        {
            //Init();
        }

        public void Init()
        {
            
        }

        public EnemyActor GetEnemy(GameObject enemyPrefab)
        {
            GameObject clone = Instantiate(enemyPrefab);
            EnemyActor enemyActor = clone.GetComponent<EnemyActor>();

            //오브젝트풀링가능

            _enemyActorList.Add(enemyActor);


            return enemyActor;
        }

        public void ReturnEnemy(EnemyActor enemyActor)
        {
            _enemyActorList.Remove(enemyActor);

            Destroy(enemyActor.gameObject);
        }




    }
}
