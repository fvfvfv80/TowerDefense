using Assets.Scripts.TowerDefenseGame.TowerBuff;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{

    public interface ITowerStatHost
    {
        void RequestUpdateStat();
    }

    public interface IStatGetter
    {
        TowerTemplateSO.WeaponSpec BaseSpec { get; }

        float AddedDamage { get; }

    }

    public class TowerStatModule : MonoBehaviour,IStatGetter
    {
        [SerializeField]
        private TowerTemplateSO towerTemplate;

        private ITowerStatHost _host;

        private int _level;

        private float _baseDamage;

        private float _rate;

        private float _range;

        private float _addedDamage;

        private readonly TowerDamageBuffStack _damageBuffStack = new();


        public float BaseDamage => _baseDamage;

        public TowerTemplateSO.WeaponSpec BaseSpec => towerTemplate.weapon[_level];

        public float AddedDamage => _addedDamage;

        public float TotalDamage => _baseDamage + _addedDamage;

        public float Rate => _rate;

        public float Range => _range;

        
        public void Init(ITowerStatHost host)
        {
            _host = host;
        }
        
        public void Setup()
        {
            ApplyLevel(0);
        }

        public void ApplyLevel(int level)
        {
            var levelData = towerTemplate.weapon[level];

            _level = level;
            _baseDamage = levelData.damage;
            _rate = levelData.rate;
            _range = levelData.range;

            UpdateStat();

        }


        //버프 받는 모듈로 따로 분리 가능성 있음

        public void ApplyBuff(TowerDamageBuff buff)
        {
            _damageBuffStack.AddBuff(buff);
            UpdateStat();
        }

        public void RemoveBuff(TowerDamageBuff buff)
        {
            _damageBuffStack.RemoveBuff(buff);
            UpdateStat();
        }

        private void UpdateStat()
        {
            _addedDamage = _baseDamage * _damageBuffStack.Multiple;

            _host.RequestUpdateStat();
        }
    }
}