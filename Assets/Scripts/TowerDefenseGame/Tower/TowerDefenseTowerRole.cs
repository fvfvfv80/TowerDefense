using Assets.Scripts.TowerDefenseGame.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerDefenseTowerRole : MonoBehaviour
    {
        [SerializeField]
        private EnemySystem enemySystem;



        public IEnumerable<BaseActor> FindTargetList()
        {
            return enemySystem.EnemyList;
        }
       
    }
}