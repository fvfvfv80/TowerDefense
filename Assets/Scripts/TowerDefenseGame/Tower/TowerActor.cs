using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerActor : BaseActor , ITowerHandler
    {
        [SerializeField]
        private TowerBaseModule towerBase;

        [SerializeField]
        private TowerWeaponModule towerWeapon;


       
    }
}