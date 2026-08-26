using Assets.Scripts.TowerDefenseGame.Tower;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TowerDefenseGame.UI
{
    public class PopupTowerUIViewer : MonoBehaviour
    {
        [SerializeField]
        private Image imageTower;

        [SerializeField]
        private TextMeshProUGUI textDamage;

        [SerializeField]
        private TextMeshProUGUI textRate;

        [SerializeField]
        private TextMeshProUGUI textRange;

        [SerializeField]
        private TextMeshProUGUI textLevel;

        [SerializeField]
        private TextMeshProUGUI textUpgradeCost;

        [SerializeField]
        private TextMeshProUGUI textSellCost;

        [SerializeField]
        private Button buttonUpgrade;

        [SerializeField]
        private TowerAttackRangeDisplayModule towerAttackRange;


        private TowerActor _currentTower;

        private void Awake()
        {
            OffPopup();
        }



        public void ShowPopup(TowerActor towerActor)
        {
            _currentTower = towerActor;

            UpdateTowerData();
            gameObject.SetActive(true);

            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.TowerStat.BaseSpec.range);
        }


        public void OffPopup()
        {
            gameObject.SetActive(false);
            towerAttackRange.OffAttackRange();
        }

        private void UpdateTowerData()
        {
            var towerType = _currentTower.TowerType;
            var towerStat = _currentTower.TowerStat; // 이걸 모듈이아니라 dto로 넘겨줄수도있음 

            if(towerType == TowerType.Cannon|| towerType == TowerType.Laser)
            {
                imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
                textDamage.text = $"Damage: {towerStat.BaseSpec.damage}"+
                                  $" + <color=red>{towerStat.AddedDamage:F1}</color>";
            }
            else
            {
                imageTower.rectTransform.sizeDelta = new Vector2(59, 59);

                if (towerType == TowerType.Slow)
                    textDamage.text = $"Slow: {towerStat.BaseSpec.slow * 100}%";
                else if (towerType == TowerType.Buff)
                    textDamage.text = $"Buff: {towerStat.BaseSpec.buff * 100}%";
            }

            imageTower.sprite = _currentTower.Sprite;

            textRate.text = $"Rate: {towerStat.BaseSpec.rate}";
            textRange.text = $"Range: {towerStat.BaseSpec.range}";
            textLevel.text = $"Level: {_currentTower.Level + 1}";


            textUpgradeCost.text = $"{_currentTower.UpgradeCost}";
            textSellCost.text = $"{_currentTower.SellCost}";

            buttonUpgrade.interactable = _currentTower.Level + 1 < _currentTower.MaxLevel;
        }

        public void UpdatePopup()
        {
            UpdateTowerData();
            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.TowerStat.BaseSpec.range);
        }

    }
}