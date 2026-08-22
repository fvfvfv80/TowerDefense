using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.TowerDefenseGame.Tower.TowerTemplateSO;


namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public enum TowerType
    {
        Cannon,
        Laser,
        Slow
    }
    public class TowerActor : BaseActor, ITowerWeaponHost
    {
        public TowerType towerType;
        [SerializeField] 
        private TowerTemplateSO towerTemplate;
        [SerializeField] 
        private SpriteRenderer towerRenderer;
        [SerializeField] 
        private TowerFeatureModule[] featureModules;

        private TowerContext _towerContext;
        private int _towerLevel;

        public int Level => _towerLevel;

        public int MaxLevel => towerTemplate.weapon.Length;

        public bool IsMaxLevel => _towerLevel == MaxLevel - 1;

        public WeaponSpec CurrentSpec => towerTemplate.weapon[_towerLevel];

        public int UpgradeCost => towerTemplate.weapon[Mathf.Min(MaxLevel - 1, _towerLevel + 1)].cost;

        public int SellGold => towerTemplate.weapon[_towerLevel].sell;

        public Sprite TowerSprite => towerRenderer.sprite;


        public void Init()
        {
            foreach (var featureModule in featureModules)
            {
                featureModule.Init(this);
                featureModule.ApplyLevel(_towerLevel);
            }
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
            foreach (var featureModule in featureModules)
                featureModule.StartFeature();
        }

        public void UpgradeTower()
        {
            _towerLevel = Mathf.Min(MaxLevel - 1, _towerLevel + 1);

            foreach (var featureModule in featureModules)
                featureModule.ApplyLevel(_towerLevel);

            towerRenderer.sprite = towerTemplate.weapon[_towerLevel].sprite;
        }

        public void ReleaseContext()
        {
            foreach (var featureModule in featureModules)
                featureModule.Release();

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