using Assets.Scripts.TowerDefenseGame.Tower;
using System;
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
        private TowerAttackRangeDisplayModule towerAttackRange;

        private TowerWeaponModule _currentTower;

        private void Awake()
        {
            OffPopup();
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                OffPopup();
        }
        public void OnPopup(Transform towerTransform)
        {
            _currentTower = towerTransform.GetComponent<TowerWeaponModule>();

            UpdatetowerData();
            gameObject.SetActive(true);

            towerAttackRange.OnAttackRange(_currentTower.transform.position, _currentTower.Range);
        }

        public void OffPopup()
        {
            gameObject.SetActive(false);
            towerAttackRange.OffAttackRange();
        }

        private void UpdatetowerData()
        {
            textDamage.text = $"Damage: {_currentTower.Damage}";
            textRate.text = $"Rate: {_currentTower.Rate}";
            textRange.text = $"Range: {_currentTower.Range}";
            textLevel.text = $"Level: {_currentTower.Level}";
        }


    }
}