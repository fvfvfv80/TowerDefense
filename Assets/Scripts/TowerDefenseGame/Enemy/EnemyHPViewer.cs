using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyHPViewer : MonoBehaviour
    {
        private EnemyHPModule _enemyHP;

        private Slider _slider;

        public void Setup(EnemyHPModule enemyHP)
        {
            _slider = GetComponent<Slider>();
            _enemyHP = enemyHP;

        }
        private void Update()
        {
            //이벤트를 받아서 갱신하는 방식으로 바꿀수도잇음
            _slider.value = _enemyHP.CurrentHP / _enemyHP.MaxHP;
        }

    }

}

