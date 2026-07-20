using Assets.Scripts.TowerDefenseGame.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame
{
    public class TowerDefenseFlow : MonoBehaviour
    {

        [SerializeField]
        private PlayerHPModule _playerHP;

        [SerializeField]
        private PlayerGoldModule _playerGold;


        public void AddPlayerGold(int gold)
        {
            _playerGold.CurrentGold += gold;
        }

        public void NotifyEnemyGoal()
        {
            _playerHP.TakeDamage(1);
        }
    }
}