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

        public bool CheckGoldEnough(int gold)//이런건 시스템에 들어가거나 할 수도 있겠군
        {
            if (gold > currentGold)
                return false;

            return true;
        }
    }
}