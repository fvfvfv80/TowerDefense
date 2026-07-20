using Assets.Scripts.TowerDefenseGame.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame
{
    public class StageUIViewer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textPlayerHP;

        [SerializeField]
        private PlayerHPModule playerHP;

        [SerializeField]
        private TextMeshProUGUI textPlayerGold;

        [SerializeField]
        private PlayerGoldModule playerGold;

        private void Update()
        {
            textPlayerHP.text = $"{playerHP.CurrentHP}/{playerHP.MaxHP}";

            textPlayerGold.text = $"{playerGold.CurrentGold}";
        }
        
    }
}