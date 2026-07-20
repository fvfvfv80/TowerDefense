using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public class PlayerGoldModule : MonoBehaviour
    {

        [SerializeField]
        private int currentGold = 100;

        public int CurrentGold
        {
            get => currentGold;
            set => currentGold = Mathf.Max(0, value);
        }
    }
}