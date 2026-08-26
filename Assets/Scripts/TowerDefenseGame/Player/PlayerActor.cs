using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerActor : BaseActor
    {

        //직렬화 가능
        private PlayerHPModule _playerHP;

        private PlayerGoldModule _playerGold;

        public PlayerHPModule HPModule => _playerHP;

        public PlayerGoldModule GoldModule => _playerGold;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _playerHP = GetComponent<PlayerHPModule>();
            _playerGold = GetComponent<PlayerGoldModule>();
        }

    }
}