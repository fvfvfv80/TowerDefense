using Assets.Scripts.TowerDefenseGame.Tower;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.TowerWeaponModule.Range);
        }


        public void OffPopup()
        {
            gameObject.SetActive(false);
            towerAttackRange.OffAttackRange();
        }

        private void UpdatetowerData()
        {
            var towerWeapon = _currentTower.TowerWeaponModule;
            //var towerBase = _currentTower.TowerBaseModule;
            imageTower.sprite = _currentTower.TowerSprite;

            textDamage.text = $"Damage: {towerWeapon.Damage}";
            textRate.text = $"Rate: {towerWeapon.Rate}";
            textRange.text = $"Range: {towerWeapon.Range}";
            textLevel.text = $"Level: {_currentTower.Level + 1}";

            buttonUpgrade.interactable = _currentTower.Level + 1 < _currentTower.MaxLevel;
        }

        public void UpdatePopup()
        {
            UpdatetowerData();
            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.TowerWeaponModule.Range);
        }

    }
}