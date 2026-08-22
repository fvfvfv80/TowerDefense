using Assets.Scripts.TowerDefenseGame.Tower.TowerWeapon;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerActor : BaseActor, ITowerWeaponHost
    {
        [SerializeField]
        private TowerTemplateSO towerTemplate;

        [SerializeField]
        private SpriteRenderer towerRenderer;


        [SerializeField]
        private TowerWeaponModule _towerWeaponModule;




        private TowerContext _towerContext;


        private int _towerLevel;



        public int Level => _towerLevel;

        public int MaxLevel => towerTemplate.weapon.Length;

        public int UpgradeCost => towerTemplate.weapon[Mathf.Min(MaxLevel - 1, _towerLevel + 1)].cost;

        public int SellGold => towerTemplate.weapon[_towerLevel].sell;

        public Sprite TowerSprite => towerRenderer.sprite;

        public TowerWeaponModule TowerWeaponModule => _towerWeaponModule;


   

        public void Init()
        {
            _towerWeaponModule.Init(this);

        }

        public void Setup()
        {
 
        }

        public void SetupContext(TowerContext towerContext)
        {
            _towerContext = towerContext;
        }

        public void StartTower()
        {
            _towerWeaponModule.StartTower();
        }
        public void UpgradeTower()
        {
            _towerLevel = Mathf.Min(MaxLevel - 1, _towerLevel + 1);
            _towerWeaponModule.SetWeaponLevel(_towerLevel);
            towerRenderer.sprite = towerTemplate.weapon[_towerLevel].sprite;


        }


        public void ReleaseContext()
        {
            _towerContext.Release();
        }

        #region TowerHandle

        public IEnumerable<BaseActor> RequestTowerTargetList()
        {
            return _towerContext.FindTargetList();
        }

        #endregion
    }
}