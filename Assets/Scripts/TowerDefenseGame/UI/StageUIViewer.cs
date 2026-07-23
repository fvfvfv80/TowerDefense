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
        private EnemyWaveSequenceFlow waveSequenceFlow;

        [SerializeField]
        private TextMeshProUGUI textEnemy;

        [SerializeField]
        private EnemyWaveFlow enemyWaveFlow;


        private void Update()
        {
            textPlayerHP.text = $"{playerHP.CurrentHP}/{playerHP.MaxHP}";

            textPlayerGold.text = $"{playerGold.CurrentGold}";

            textWave.text = $"{waveSequenceFlow.CurrentWaveCount}/{waveSequenceFlow.MaxWave}";

            textEnemy.text = $"{enemyWaveFlow.CurrentEnemyCount}/{enemyWaveFlow.MaxEnemyCount}";
        }
        
    }
}