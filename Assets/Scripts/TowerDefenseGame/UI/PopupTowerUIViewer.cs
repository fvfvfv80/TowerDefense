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

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                OffPopup();
        }

        //이름이..
        public void OnPopup(TowerActor towerActor)
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
            var towerBase = _currentTower.towerBaseModule;
            imageTower.sprite = towerBase.TowerImage;

            textDamage.text = $"Damage: {towerWeapon.Damage}";
            textRate.text = $"Rate: {towerWeapon.Rate}";
            textRange.text = $"Range: {towerWeapon.Range}";
            textLevel.text = $"Level: {towerBase.Level}";

            buttonUpgrade.interactable = towerBase.Level < towerBase.MaxLevel;
        }

        public void UpdatePopup()
        {
            UpdatetowerData();
            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.TowerWeaponModule.Range);
        }

    }
}