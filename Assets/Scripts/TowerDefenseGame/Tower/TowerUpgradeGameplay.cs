using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{

    // 모듈을 확장하는 것을 게임플레이라고 할까 모듈이라 할까.. 강화 기능이면 게임플레이가 맞는것 같기도하고
    public class TowerUpgradeGameplay : MonoBehaviour
    {
        [SerializeField]
        private TowerBaseModule towerBaseModule;
        [SerializeField]
        private TowerWeaponModule towerWeaponModule;


        public void UpgradeTower()
        {


            towerBaseModule.UpgradeLevel();

            var towerWeaponData = towerBaseModule.CurrentTowerWeaponData;
          

            var weaponStat = new TowerWeaponStat()
            {
                Damage = towerWeaponData.damage,
                Rate = towerWeaponData.rate,
                Range = towerWeaponData.range
            };

            towerWeaponModule.SetWeaponStat(weaponStat);

            //이미지 업그레이드 용 //렌더러나 애니메이션 모듈로 빼면 그걸가져와야함
            towerBaseModule.ChangeTowerSprite(towerWeaponData.sprite);
        }
    }
}