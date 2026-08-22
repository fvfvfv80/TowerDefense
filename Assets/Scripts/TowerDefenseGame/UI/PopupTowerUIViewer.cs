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

            UpdatetowerData();
            gameObject.SetActive(true);

            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.CurrentSpec.range);
        }


        public void OffPopup()
        {
            gameObject.SetActive(false);
            towerAttackRange.OffAttackRange();
        }

        private void UpdatetowerData()
        {
            var towerType = _currentTower.towerType;
            var towerSpec = _currentTower.CurrentSpec;

            if(towerType == TowerType.Cannon|| towerType == TowerType.Laser)
            {
                imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
                textDamage.text = $"Damage: {towerSpec.damage}";
            }
            else
            {
                imageTower.rectTransform.sizeDelta = new Vector2(59, 59);
                textDamage.text = $"Slow: {towerSpec.slow * 100}%";
            }

            imageTower.sprite = _currentTower.TowerSprite;

            textRate.text = $"Rate: {towerSpec.rate}";
            textRange.text = $"Range: {towerSpec.range}";
            textLevel.text = $"Level: {_currentTower.Level + 1}";

            buttonUpgrade.interactable = _currentTower.Level + 1 < _currentTower.MaxLevel;
        }

        public void UpdatePopup()
        {
            UpdatetowerData();
            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.CurrentSpec.range);
        }

    }
}