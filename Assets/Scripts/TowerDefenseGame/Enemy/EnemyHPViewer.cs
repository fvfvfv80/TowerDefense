using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyHPViewer : MonoBehaviour
    {

        private Slider _slider;

        public void Setup(EnemyHPModule enemyHP)
        {
            _slider = GetComponent<Slider>();

            enemyHP.OnHpChanged += UpdateHPView;

            UpdateHPView(enemyHP);

        }

        //인터페이스로 가능 IHPModule
        private void UpdateHPView(EnemyHPModule enemyHP) 
        {
            _slider.value = enemyHP.CurrentHP / enemyHP.MaxHP;
        }
       

    }

}

