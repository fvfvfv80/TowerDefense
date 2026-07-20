using Assets.Scripts.TowerDefenseGame.Player;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame
{
    public class DefenseGameSystem : MonoBehaviour
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