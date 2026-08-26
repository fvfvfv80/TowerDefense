using Assets.Scripts.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{



    public class TowerBuffDirector : MonoBehaviour
    {


        private readonly HashSet<TowerActor> _towerSet = new();

        public List<TowerActor> CurrentTowers => _towerSet.ToList();

        public void Init()
        {

        }



        //타워가 만들어지고 나서 호출 해당 타워가 버프 범위내인지 검사하고 버프 부여
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