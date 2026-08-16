using Assets.Scripts.TowerDefenseGame.EnemyWave;
using Assets.Scripts.TowerDefenseGame.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.UI
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

        [SerializeField]
        private TextMeshProUGUI textWave;

        [SerializeField]
        private TextMeshProUGUI textEnemy;

        [SerializeField]
        private EnemyWaveGameplay enemyWaveGameplay;


        private void Update()
        {
            textPlayerHP.text = $"{playerHP.CurrentHP}/{playerHP.MaxHP}";

            textPlayerGold.text = $"{playerGold.CurrentGold}";

            textWave.text = $"{enemyWaveGameplay.CurrentWaveCount}/{enemyWaveGameplay.MaxWave}";

            textEnemy.text = $"{enemyWaveGameplay.CurrentEnemyCount}/{enemyWaveGameplay.MaxWaveEnemyCount}";
        }
        
    }
}