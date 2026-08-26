using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{



    public class TowerBuffDirector : MonoBehaviour
    {

        private readonly HashSet<TowerActor> _towerSet = new();

        public List<TowerActor> CurrentTowers => _towerSet.ToList();

        public void Init() { }



        public void RegisterTower(TowerActor towerActor)
        {
            foreach (var tower in _towerSet)
            {
                tower.ApplyBuff(towerActor);
            }
            _towerSet.Add(towerActor);
        }

        public void RemoveTower(TowerActor towerActor)
        {
            _towerSet.Remove(towerActor);
        }



    }
}