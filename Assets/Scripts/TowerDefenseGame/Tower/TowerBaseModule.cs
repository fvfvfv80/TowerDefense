using System;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerBaseModule : MonoBehaviour
    {
        [SerializeField]
        private TowerTemplateSO towerTemplate;

        [SerializeField]
        private SpriteRenderer towerRenderer;

        private int _level;

        public int Level => _level + 1;

        public Sprite TowerImage => towerRenderer.sprite;

        public int MaxLevel => towerTemplate.weapon.Length;

        public int UpgradeCost => towerTemplate.weapon[Math.Min(MaxLevel - 1, _level + 1)].cost;

        public int SellGold => towerTemplate.weapon[_level].sell;

        public TowerTemplateSO TowerTemplate => towerTemplate;

        public void ChangeTowerSprite(int level)
        {
            towerRenderer.sprite = towerTemplate.weapon[level].sprite;
        }

        public void Upgrade()
        {
            _level++;
            ChangeTowerSprite(_level);

        }


        
    }
}