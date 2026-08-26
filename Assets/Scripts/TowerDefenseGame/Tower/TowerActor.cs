using Assets.Scripts.TowerDefenseGame.Tower.Feature;
using Assets.Scripts.TowerDefenseGame.Tower.Gameplay;
using Assets.Scripts.TowerDefenseGame.Weapon;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public enum TowerType
    {
        Cannon = 0,
        Laser,
        Slow,
        Buff
    }

    public class TowerActor : BaseActor, ITowerSupportHost, IWeaponModuleHost, ITowerStatHost
    {
        [SerializeField]
        private TowerType towerType;

        [SerializeField] 
        private TowerTemplateSO towerTemplate;

        [SerializeField] 
        private SpriteRenderer towerRenderer;

        [SerializeField]
        private TowerStatModule towerStatModule;

        [SerializeField]
        private TowerGameplay towerGameplay;

        private TowerRoleFlow _towerRole;

        private int _towerLevel;

        public TowerType TowerType => towerType;

        public int Level => _towerLevel;

        public int MaxLevel => towerTemplate.weapon.Length;

        public bool IsMaxLevel => _towerLevel == MaxLevel - 1;


        public int UpgradeCost => towerTemplate.weapon[Mathf.Min(MaxLevel - 1, _towerLevel + 1)].cost;

        public int SellCost => towerTemplate.weapon[_towerLevel].sell;

        public Sprite Sprite => towerRenderer.sprite;

        public TowerStatModule StatModule => towerStatModule;

        public IStatGetter TowerStat => towerStatModule;


        public void Init()
        {
            towerStatModule.Init(this);
            towerGameplay.Init(this);
        }

        public void Setup()
        {
            towerStatModule.Setup();
            towerGameplay.Setup();
        }

        public void SetupRole(TowerRoleFlow towerRole)
        {
            _towerRole = towerRole;
        }

        public void StartTower()
        {
            towerGameplay.StartGameplay();
        }

        public void UpgradeTower()
        {
            _towerLevel = Mathf.Min(MaxLevel - 1, _towerLevel + 1);

            towerStatModule.ApplyLevel(_towerLevel);

            towerGameplay.ApplyLevel(_towerLevel);

            towerRenderer.sprite = towerTemplate.weapon[_towerLevel].sprite;
        }

        public void ApplyBuff(TowerActor anotherTower)
        {
            towerGameplay.HandleCommand(new TowerGameplayCommand()
            {
                type = TowerGameplayCommandType.ApplyBuff,
                buffTargetTower = anotherTower
            });
        }

        public void Release()
        {
            towerGameplay.Release();


        }


        #region TowerModuleHandle


        public IEnumerable<BaseActor> RequestBuffTargetList()
        {
            return _towerRole.FindBuffTargetList();
        }

        public IEnumerable<BaseActor> RequestTargetList()
        {
            return _towerRole.FindAttackTargetList();
        }

        public void RequestUpdateStat()
        {
            towerGameplay.UpdateStat();
        }

        #endregion
    }
}